using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using WanderKiwi.Application.Configuration;
using WanderKiwi.Application.DTOs;
using WanderKiwi.Application.Interfaces;

namespace WanderKiwi.Application.Services;

public class GroqTripGenerationService : ITripGenerationService
{
    private readonly IDestinationRepository _destinationRepository;
    private readonly IAttractionRepository _attractionRepository;
    private readonly HttpClient _httpClient;
    private readonly GroqApiOptions _groqOptions;

    public GroqTripGenerationService(
        IDestinationRepository destinationRepository,
        IAttractionRepository attractionRepository,
        HttpClient httpClient,
        IOptions<GroqApiOptions> groqOptions)
    {
        _destinationRepository = destinationRepository;
        _attractionRepository = attractionRepository;
        _httpClient = httpClient;
        _groqOptions = groqOptions.Value;
    }

    public async Task<GeneratedTripItineraryDto> GenerateItineraryAsync(
        GenerateTripRequestDto request)
    {
        
        // 1. Validate request
        if (request.Destination == null)
            throw new ArgumentException("Destination must be provided in the request.");

        if (request.StartDate.Date > request.EndDate.Date)
            throw new ArgumentException("Start date cannot be after end date.");

        if (request.Travellers <= 0)
            throw new ArgumentException("Number of travelers must be greater than zero.");

        var destinationForId = _destinationRepository.GetbyNameAsync(request.Destination);
        request.DestinationId = destinationForId.Result.Id;

        // 2. Fetch destination
        var destination =
            await _destinationRepository.GetByIdAsync(request.DestinationId);

        if (destination == null)
        {
            throw new ArgumentException(
                $"Destination with ID {request.DestinationId} not found.");
        }

        // 3. Fetch database attractions.
        // These are used for matching/enrichment after AI generation.
        var dbAttractions =
            await _attractionRepository.GetByDestinationIdAsync(request.DestinationId);

        // 4. Calculate trip duration
        var totalDays =
            Math.Max(
                1,
                (int)(request.EndDate.Date - request.StartDate.Date).TotalDays + 1);

        // 5. Build AI prompt
        var systemPrompt = BuildSystemPrompt(
            request,
            destination.Name,
            totalDays);

        // 6. Build Groq request
        var requestBody = new
        {
            model = _groqOptions.Model,

            messages = new[]
            {
                new
                {
                    role = "user",
                    content =
                        $"{systemPrompt}\n\n" +
                        $"{BuildSystemPrompt(request, destination.Name, totalDays)}"
                }
            },

            temperature = 0.6,
            max_completion_tokens = 6000,
            reasoning_effort = "low",
            include_reasoning = false
        };

        var json = JsonSerializer.Serialize(requestBody);

        using var content = new StringContent(
            json,
            Encoding.UTF8,
            "application/json");

        // 7. Create HTTP request
        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Post,
            _groqOptions.BaseUrl);

        httpRequest.Headers.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                _groqOptions.ApiKey);

        httpRequest.Content = content;

        // 8. Call Groq
        using var response =
            await _httpClient.SendAsync(httpRequest);

        var responseString =
            await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(
                $"Groq API request failed. " +
                $"Status: {(int)response.StatusCode}, " +
                $"Response: {responseString}");
        }

        // 9. Extract AI content
        using var document =
            JsonDocument.Parse(responseString);



Console.WriteLine("========== GROQ RESPONSE ==========");
Console.WriteLine(responseString);
Console.WriteLine("===================================");

var root = document.RootElement;

if (!root.TryGetProperty("choices", out var choices))
{
    throw new InvalidOperationException(
        $"Groq response does not contain 'choices'. Response: {responseString}");
}

if (choices.GetArrayLength() == 0)
{
    throw new InvalidOperationException(
        $"Groq returned an empty 'choices' array. Response: {responseString}");
}

var choice = choices[0];

if (!choice.TryGetProperty("message", out var message))
{
    throw new InvalidOperationException(
        $"Groq response does not contain 'message'. Response: {responseString}");
}

if (!message.TryGetProperty("content", out var contentElement))
{
    throw new InvalidOperationException(
        $"Groq response does not contain 'content'. Response: {responseString}");
}

// var aiJsonOutput = contentElement.GetString();

// if (string.IsNullOrWhiteSpace(aiJsonOutput))
// {
//     throw new InvalidOperationException(
//         $"Groq returned an empty content value. Full response: {responseString}");
// }
//-----

        // var aiJsonOutput1 =
        //     document.RootElement
        //         .GetProperty("choices")[0]
        //         .GetProperty("message")
        //         .GetProperty("content")
        //         .GetString();


        // if (string.IsNullOrWhiteSpace(aiJsonOutput1))
        // {
        //     throw new InvalidOperationException(
        //         "Groq returned an empty itinerary.");
        // }

        //----

    //     var choice = document.RootElement
    // .GetProperty("choices")[0];

var finishReason = choice
    .GetProperty("finish_reason")
    .GetString();

Console.WriteLine($"Groq finish reason: {finishReason}");

var aiJsonOutput = contentElement.GetString();

if (string.IsNullOrWhiteSpace(aiJsonOutput))
{
    throw new InvalidOperationException(
        $"Groq returned an empty content value. Full response: {responseString}");
}

        // 10. Deserialize AI JSON
        var serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        GeneratedTripItineraryDto? itinerary;

        try
        {
            itinerary =
                JsonSerializer.Deserialize<GeneratedTripItineraryDto>(
                    aiJsonOutput,
                    serializerOptions);
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException(
                "Groq returned itinerary JSON in an unexpected format.",
                ex);
        }

        if (itinerary == null)
        {
            throw new InvalidOperationException(
                "Failed to deserialize the generated itinerary.");
        }

        // 11. Apply request metadata in case the model omitted or altered it.
        itinerary.DestinationName = destination.Name;
        itinerary.StartDate = request.StartDate.Date;
        itinerary.EndDate = request.EndDate.Date;
        itinerary.TotalDays = totalDays;
        itinerary.Travelers = request.Travellers;
        itinerary.TripStyle = request.TripStyle;
        itinerary.Interests = request.Interests ?? [];
        itinerary.Budget = request.Budget;
        itinerary.TransportMode = request.TransportMode;

        // 12. Match AI attractions against WanderKiwi database.
        // Matching is optional: an attraction does not need to exist in the DB.
        HydrateItineraryWithDatabaseDetails(
            itinerary,
            dbAttractions);

        // 13. Validate basic itinerary structure.
        ValidateItinerary(itinerary, totalDays);

        return itinerary;
    }

    private static string BuildSystemPrompt(
        GenerateTripRequestDto request,
        string destinationName,
        int totalDays)
    {
//         var interests = request.Interests is { Count: > 0 }
//             ? string.Join(", ", request.Interests)
//             : "No specific interests provided";

//         return $$"""
// You are WanderKiwi's AI Trip Planner.

// Your job is to create a realistic, practical and personalized travel itinerary
// for a trip in New Zealand.

// Return ONLY valid JSON.
// Do not return Markdown.
// Do not use ```json code fences.
// Do not include explanations outside the JSON.
// Do not generate images.
// Do not include image URLs.

// TRIP INFORMATION

// Destination:
// {{destinationName}}

// Start date:
// {{request.StartDate:yyyy-MM-dd}}

// End date:
// {{request.EndDate:yyyy-MM-dd}}

// Number of days:
// {{totalDays}}

// Daily start time:
// {{request.StartTime:hh\:mm}}

// Number of travellers:
// {{request.Travellers}}

// Trip style:
// {{request.TripStyle}}

// Budget:
// {{request.Budget}}

// Transport mode:
// {{request.TransportMode}}

// Interests:
// {{interests}}

// PERSONALIZATION

// You MUST use the traveller's:
// - number of travellers
// - trip style
// - interests
// - budget
// - transport mode
// - available dates
// - daily start time

// when selecting attractions, activities, restaurants and the overall pace.

// TRIP STYLE

// Adapt the itinerary to the requested trip style.

// Relaxed:
// - Fewer activities
// - More free time
// - Longer breaks
// - Comfortable driving
// - Avoid tightly packed schedules

// Balanced:
// - Mix sightseeing, activities, meals and free time
// - Moderate number of attractions
// - Comfortable pace
// - Reasonable driving

// Adventure:
// - Prioritize outdoor activities, hiking, nature and active experiences
// - More activities are acceptable
// - Include challenging activities only when appropriate

// Family:
// - Prefer family-friendly activities
// - Avoid unnecessarily difficult activities
// - Include breaks
// - Consider the number of travellers

// Luxury:
// - Prefer high-quality experiences
// - Allow premium dining and experiences
// - Use a comfortable pace

// Budget:
// - Prefer free or low-cost attractions
// - Prefer affordable meals
// - Avoid unnecessary expensive activities

// If another trip style is supplied, interpret it naturally.

// INTERESTS

// Prioritize activities that match the traveller's interests.

// Possible interests include:
// Nature, Beaches, Hiking, Adventure, Wildlife, Food, Culture,
// History, Museums, Photography, Shopping, Family Activities,
// Nightlife, Relaxation, Scenic Viewpoints, Architecture and
// Local Experiences.

// Do not fill the itinerary with generic attractions simply because
// they are popular if they do not match the traveller's interests.

// BUDGET

// Budget must influence activities, meals and experiences.

// Budget:
// - Prioritize free and inexpensive activities.
// - Prefer affordable restaurants.
// - Avoid expensive activities unless they provide significant value.

// Moderate:
// - Mix free, affordable and moderately priced experiences.
// - Prefer good-value activities and restaurants.

// Premium:
// - Allow higher-priced restaurants and experiences when appropriate.

// Luxury:
// - Prioritize premium dining and high-quality experiences.

// Do not invent exact prices unless reliable pricing information is known.
// Use general budget suitability rather than fabricated prices.

// NUMBER OF TRAVELLERS

// Consider the group size when selecting activities and restaurants.

// For larger groups:
// - Prefer group-friendly activities and restaurants.
// - Avoid activities that are impractical for the group size.

// For solo travellers or couples:
// - Include experiences appropriate for the group size.

// ITINERARY RULES

// For every day:
// - Start at or after the requested daily start time.
// - Include travel time.
// - Include driving time when using a car.
// - Include lunch.
// - Include dinner when appropriate.
// - Include reasonable breaks and free time.
// - Allow enough time to enjoy each attraction.
// - Avoid unrealistic schedules.
// - Avoid excessive driving.
// - Group geographically close locations together.
// - Minimize unnecessary backtracking.
// - Consider opening hours when known.
// - Consider seasonal conditions when relevant.
// - Mark weather-sensitive activities as weather dependent.

// ARRIVAL DAY

// If the traveller arrives at the destination at the requested start time,
// do not schedule an activity before there is enough time to reasonably
// arrive, collect transportation and reach the first activity.

// ATTRACTIONS

// You may recommend ANY suitable attraction or activity.

// The attraction does NOT need to exist in WanderKiwi's database.

// You may recommend:
// - Tourist attractions
// - Nature destinations
// - Beaches
// - Parks
// - Museums
// - Galleries
// - Scenic viewpoints
// - Hiking tracks
// - Cultural attractions
// - Restaurants
// - Cafes
// - Local experiences
// - Adventure activities
// - Landmarks

// For each attraction/activity provide:
// - Name
// - Location
// - Short description
// - Latitude when known
// - Longitude when known
// - Estimated visit duration

// Do not generate images or image URLs.

// DATABASE MATCHING

// WanderKiwi may attempt to match your recommended attraction against
// its own database after your response is generated.

// Therefore:
// - Use clear, recognizable attraction names.
// - Do not create fake attraction IDs.
// - Do not assume an attraction has a WanderKiwi database ID.
// - Do not include database IDs.

// DRIVING INFORMATION

// When transport mode is Car:
// - Provide estimated driving time from the previous location.
// - Provide estimated driving distance from the previous location.
// - These values may be approximate.
// - WanderKiwi may replace them with real routing information later.

// If driving information is unknown, use 0 rather than inventing a precise value.

// If transport mode is not Car, do not provide unnecessary driving information.

// MEALS

// Include lunch every day unless explicitly requested otherwise.

// Choose meals according to:
// - Budget
// - Trip style
// - Number of travellers
// - Location
// - Interests

// Avoid making the traveller travel a long distance just for lunch.

// WEATHER

// Outdoor activities may be weather dependent.

// Set "weatherDependent" to true for activities such as:
// - Hiking
// - Beaches
// - Scenic viewpoints
// - Outdoor walks
// - Water activities
// - Outdoor adventure activities

// JSON OUTPUT

// Return ONLY valid JSON using this structure:

// {
//   "tripName": "string",
//   "destinationName": "string",
//   "startDate": "YYYY-MM-DD",
//   "endDate": "YYYY-MM-DD",
//   "totalDays": 0,
//   "travelers": 0,
//   "tripStyle": "string",
//   "interests": ["string"],
//   "budget": "string",
//   "transportMode": "string",
//   "summary": "string",
//   "days": [
//     {
//       "dayNumber": 1,
//       "date": "YYYY-MM-DD",
//       "theme": "string",
//       "summary": "string",
//       "stops": [
//         {
//           "order": 1,
//           "type": "activity",
//           "startTime": "08:00",
//           "endTime": "09:00",
//           "title": "string",
//           "description": "string",
//           "attraction": {
//             "name": "string",
//             "location": "string",
//             "latitude": null,
//             "longitude": null
//           },
//           "driving": {
//             "durationMinutes": 0,
//             "distanceKm": 0
//           },
//           "durationMinutes": 60,
//           "weatherDependent": false
//         }
//       ]
//     }
//   ]

// IMPORTANT:
// - "type" must be one of: activity, meal, travel, free_time.
// - Do not generate attraction IDs.
// - "attractionId" must always be null in your AI response.
// - "isFromDatabase" must always be false in your AI response.
// - "imageUrl" must always be null in your AI response.
// - "dataSource" must always be "ai" in your AI response.
// - WanderKiwi's backend will replace these values when a database match exists.
// """;
//     }


// var interests = request.Interests is { Count: > 0 }
//         ? string.Join(", ", request.Interests)
//         : "No specific interests";

//     return $$"""
// Create a {{totalDays}}-day itinerary for:

// Destination: {{destinationName}}
// Dates: {{request.StartDate:yyyy-MM-dd}} to {{request.EndDate:yyyy-MM-dd}}
// Daily start time: {{request.StartTime:hh\:mm}}
// Travellers: {{request.Travellers}}
// Trip style: {{request.TripStyle}}
// Interests: {{interests}}
// Budget: {{request.Budget}}
// Transport: {{request.TransportMode}}

// Make the itinerary realistic and personalized to these preferences.
// Include lunch, activities, travel time, driving estimates and reasonable breaks.
// """;

var interests = request.Interests is { Count: > 0 }
    ? string.Join(", ", request.Interests)
    : "No specific interests provided";

return $$"""
Create a realistic, practical and personalized {{totalDays}}-day itinerary
for a trip in a city or region in New Zealand.

Return ONLY valid JSON.
Do not return Markdown, explanations, images, image URLs or database IDs.

TRIP INFORMATION

Destination: {{destinationName}}
Start date: {{request.StartDate:yyyy-MM-dd}}
End date: {{request.EndDate:yyyy-MM-dd}}
Number of days: {{totalDays}}
Daily start time: {{request.StartTime:hh\:mm}}
Number of travellers: {{request.Travellers}}
Trip style: {{request.TripStyle}}
Budget: {{request.Budget}}
Transport: {{request.TransportMode}}
Interests: {{interests}}

PERSONALIZATION

Use the travellers, trip style, interests, budget, transport,
dates and start time when planning the itinerary.

Trip style should control the pace and type of activities:
- Relaxed: fewer activities, more free time and longer breaks.
- Balanced: mix sightseeing, activities, meals and free time.
- Adventure: prioritize outdoor, nature and active experiences.
- Family: prioritize family-friendly activities and breaks.
- Luxury: prioritize high-quality experiences and premium options.
- Budget: prioritize free or low-cost activities and meals.

Budget should influence activities and meals.
Do not invent exact prices.

ITINERARY RULES

For every day:
- Start at or after the requested start time.
- Include lunch every day.
- Include dinner when appropriate.
- Include activities, travel time and reasonable breaks.
- Allow enough time to enjoy each attraction.
- Avoid unrealistic schedules, excessive driving and unnecessary backtracking.
- Group geographically close attractions together.
- Consider opening hours and seasonal conditions when relevant.
- Mark outdoor/weather-sensitive activities with weatherDependent=true.

ATTRACTIONS

You may recommend any suitable attraction or activity, even if it is not
in the WanderKiwi database.

Use clear and recognizable attraction names.

For attractions provide:
- name
- location
- description
- latitude when known
- longitude when known
- estimated visit duration

Do not generate attraction IDs or images.

When transport is Car:
- Provide estimated driving time from the previous location.
- Provide estimated driving distance.
- These are approximate and may be replaced by real routing information.

Do not invent unnecessarily precise driving information.

MEALS

Include lunch every day.
Choose meals according to the traveller's budget, trip style,
interests, group size and location.
Avoid unnecessary travel just for a meal.

JSON OUTPUT

Return ONLY valid JSON using this structure:

{
  "tripName": "string",
  "destinationName": "string",
  "startDate": "YYYY-MM-DD",
  "endDate": "YYYY-MM-DD",
  "totalDays": 0,
  "travelers": 0,
  "tripStyle": "string",
  "interests": ["string"],
  "budget": "string",
  "transportMode": "string",
  "summary": "string",
  "days": [
    {
      "dayNumber": 1,
      "date": "YYYY-MM-DD",
      "theme": "string",
      "summary": "string",
      "stops": [
        {
          "order": 1,
          "type": "activity",
          "startTime": "08:00",
          "endTime": "09:00",
          "title": "string",
          "description": "string",
          "attraction": {
            "name": "string",
            "location": "string",
            "latitude": null,
            "longitude": null
          },
          "driving": {
            "durationMinutes": 0,
            "distanceKm": 0
          },
          "durationMinutes": 60,
          "weatherDependent": false
        }
      ]
    }
  ]
}

IMPORTANT:
- type must be: activity, meal, travel or free_time.
""";
}


//     private static string BuildUserPrompt(
//         GenerateTripRequestDto request,
//         string destinationName,
//         int totalDays)
//     {
//         var interests = request.Interests is { Count: > 0 }
//             ? string.Join(", ", request.Interests)
//             : "No specific interests";

//         return $$"""
// Create a {{totalDays}}-day itinerary for:

// Destination: {{destinationName}}
// Dates: {{request.StartDate:yyyy-MM-dd}} to {{request.EndDate:yyyy-MM-dd}}
// Daily start time: {{request.StartTime:hh\:mm}}
// Travellers: {{request.Travellers}}
// Trip style: {{request.TripStyle}}
// Budget: {{request.Budget}}
// Transport: {{request.TransportMode}}
// Interests: {{interests}}

// Include lunch, travel time, driving time when applicable, activities,
// reasonable breaks and realistic timing.
// """;
//     }

    private static void HydrateItineraryWithDatabaseDetails(
        GeneratedTripItineraryDto itinerary,
        List<Domain.Entities.Attraction> dbAttractions)
    {
        if (dbAttractions == null || dbAttractions.Count == 0)
            return;

        foreach (var day in itinerary.Days)
        {
            foreach (var stop in day.Stops)
            {
                if (stop.Attraction == null ||
                    string.IsNullOrWhiteSpace(stop.Attraction.Name))
                {
                    continue;
                }

                var attraction =
                    FindMatchingAttraction(
                        stop.Attraction.Name,
                        dbAttractions);

                if (attraction == null)
                {
                    stop.IsFromDatabase = false;
                    stop.DataSource = "ai";
                    continue;
                }

                stop.IsFromDatabase = true;
                stop.DataSource = "database";
                stop.AttractionId = attraction.Id;
                stop.Title = attraction.Name;
                stop.Description =
                    string.IsNullOrWhiteSpace(attraction.Description)
                        ? stop.Description
                        : attraction.Description;

                stop.ImageUrl = attraction.ImageUrl;
                stop.Latitude = attraction.Latitude;
                stop.Longitude = attraction.Longitude;

                // Keep the matched database attraction in the nested object too.
                stop.Attraction.Name = attraction.Name;
                stop.Attraction.Latitude = attraction.Latitude;
                stop.Attraction.Longitude = attraction.Longitude;
            }
        }
    }

    private static Domain.Entities.Attraction? FindMatchingAttraction(
        string aiAttractionName,
        List<Domain.Entities.Attraction> dbAttractions)
    {
        var normalizedAiName =
            NormalizeAttractionName(aiAttractionName);

        // Exact normalized match first.
        var exactMatch =
            dbAttractions.FirstOrDefault(a =>
                NormalizeAttractionName(a.Name) == normalizedAiName);

        if (exactMatch != null)
            return exactMatch;

        // Conservative containment matching.
        // This handles names such as:
        // "Te Rewa Rewa Bridge" vs "Te Rewa Rewa Bridge, New Plymouth"
        return dbAttractions.FirstOrDefault(a =>
        {
            var normalizedDbName =
                NormalizeAttractionName(a.Name);

            return normalizedAiName.Contains(normalizedDbName) ||
                   normalizedDbName.Contains(normalizedAiName);
        });
    }

    private static string NormalizeAttractionName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        var chars = value
            .ToLowerInvariant()
            .Where(char.IsLetterOrDigit)
            .ToArray();

        return new string(chars);
    }

    private static void ValidateItinerary(
        GeneratedTripItineraryDto itinerary,
        int expectedDays)
    {
        if (string.IsNullOrWhiteSpace(itinerary.TripName))
            throw new InvalidOperationException(
                "Generated itinerary is missing TripName.");

        if (itinerary.Days == null || itinerary.Days.Count == 0)
            throw new InvalidOperationException(
                "Generated itinerary contains no days.");

        if (itinerary.Days.Count != expectedDays)
        {
            throw new InvalidOperationException(
                $"Groq returned {itinerary.Days.Count} days, " +
                $"but {expectedDays} days were requested.");
        }

        foreach (var day in itinerary.Days)
        {
            if (day.Stops == null || day.Stops.Count == 0)
            {
                throw new InvalidOperationException(
                    $"Day {day.DayNumber} contains no itinerary stops.");
            }

            foreach (var stop in day.Stops)
            {
                if (string.IsNullOrWhiteSpace(stop.Type))
                {
                    throw new InvalidOperationException(
                        $"Day {day.DayNumber}, stop {stop.Order} is missing its type.");
                }

                if (string.IsNullOrWhiteSpace(stop.Title))
                {
                    throw new InvalidOperationException(
                        $"Day {day.DayNumber}, stop {stop.Order} is missing its title.");
                }
            }
        }
    }
}
