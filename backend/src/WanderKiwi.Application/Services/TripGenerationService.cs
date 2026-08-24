using WanderKiwi.Application.DTOs;
using WanderKiwi.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using WanderKiwi.Domain.Entities;

namespace WanderKiwi.Application.Services;

public class TripGenerationService : ITripGenerationService
{
    private readonly IDestinationRepository _destinationRepository;
    private readonly IAttractionRepository _attractionRepository;

    private readonly IRouteService _routeService;
    private readonly IMemoryCache _cache;

    public TripGenerationService(
        IDestinationRepository destinationRepository,
        IAttractionRepository attractionRepository,
        IRouteService routeService,
        IMemoryCache cache)
    {
        _destinationRepository = destinationRepository;
        _attractionRepository = attractionRepository;
        _routeService = routeService;
        _cache = cache;
    }

    public async Task<GeneratedTripItineraryDto> GenerateItineraryAsync(GenerateTripRequestDto request)
    {
        // 1. Fetch Destination details
        if (request.DestinationId <= 0)
            throw new ArgumentException("Invalid Destination ID.");

        var destination = await _destinationRepository.GetByIdAsync(request.DestinationId);

        if (destination == null)
            throw new ArgumentException($"Destination with ID {request.DestinationId} not found.");

        // 2. Fetch candidate attractions for this destination
        var attractions = await _attractionRepository.GetByDestinationIdAsync(request.DestinationId);

        // Filter by selected interests/categories if specified
        var candidateAttractions = attractions.ToList();
        if (request.Interests != null && request.Interests.Any())
        {
            candidateAttractions = candidateAttractions.Where(a => a.AttractionCategories
                .Any(ac => request.Interests.Contains(ac.Category.Name))).ToList();
        }



        // Fallback: If category filter is too strict, grab all attractions for this destination
        if (!candidateAttractions.Any())
        {
            candidateAttractions = attractions.ToList();
        }

        // 3. Calculate trip duration in days
        int totalDays = (int)(request.EndDate.Date - request.StartDate.Date).TotalDays + 1;
        totalDays = Math.Max(1, totalDays);

        var response = new GeneratedTripItineraryDto
        {
            TripName = $"{destination.Name} {request.TripStyle} Gateway",
            DestinationName = destination.Name,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TotalDays = totalDays,
            Days = new List<TripDayItineraryDto>()
        };

        // Shuffle attractions deterministically or by rating to give a balanced itinerary
        var availableQueue = candidateAttractions
            .OrderByDescending(a => a.Rating)
            .ToList();

        // 4. Allocate stops per day (2-3 stops per day depending on duration)
        for (int dayIndex = 0; dayIndex < totalDays; dayIndex++)
        {
            var dayDto = new TripDayItineraryDto
            {
                DayNumber = dayIndex + 1,
                Date = request.StartDate.AddDays(dayIndex),
                Theme = $"Day {dayIndex + 1} Highlights",
                Stops = new List<TripStopItineraryDto>()
            };

            int stopsForToday = Math.Min(3, availableQueue.Count);
            var todayAttractions = availableQueue.Take(stopsForToday).ToList();
            availableQueue.RemoveRange(0, stopsForToday);

            // Start the day at 9:30 AM
            DateTime currentStartTime = request.StartDate.AddDays(dayIndex).AddHours(9).AddMinutes(30);

            for (int i = 0; i < todayAttractions.Count; i++)
            {
                var attr = todayAttractions[i];
                
                // Parse recommended duration (e.g., "2 hours", "2.5 hours", "30 minutes")
                double durationHours = ParseDuration(attr.RecommendedDuration);
                DateTime currentEndTime = currentStartTime.AddHours(durationHours);

                int driveTimeMinutes = 0;

                // If there is a next stop, calculate driving time to it
                if (i < todayAttractions.Count - 1)
                {
                    var nextAttr = todayAttractions[i + 1];
                    driveTimeMinutes = await GetCachedDriveTimeAsync(attr, nextAttr);
                }

                dayDto.Stops.Add(new TripStopItineraryDto
                {
                    Order = i + 1,
                    AttractionId = attr.Id,
                    AttractionName = attr.Name,
                    ImageUrl = attr.ImageUrl,
                    Description = attr.Description,
                    RecommendedDuration = attr.RecommendedDuration,
                    BestTime = attr.BestTime,
                    TimeSlot = $"{currentStartTime:hh:mm tt} - {currentEndTime:hh:mm tt}",
                    DriveTimeToNextMinutes = driveTimeMinutes,
                    OpeningHoursNote = attr.OpeningHoursNote,
                    BookingNote = attr.BookingNote,
                    AvailabilityNote = attr.AvailabilityNote,
                    Latitude = attr.Latitude,
                    Longitude = attr.Longitude
                });

                // Set start time for the next attraction (Current End Time + Drive Time + 15 min buffer)
                currentStartTime = currentEndTime.AddMinutes(driveTimeMinutes + 15);
            }

            response.Days.Add(dayDto);

            // Refill queue if it's a multi-day trip and we run out of unique stops
            if (!availableQueue.Any() && candidateAttractions.Any())
            {
                availableQueue = candidateAttractions.OrderBy(a => Guid.NewGuid()).ToList();
            }
        }

        return response;
    }

    /// <summary>
    /// Wrapper for IRouteService that caches the API response.
    /// </summary>
    private async Task<int> GetCachedDriveTimeAsync(Attraction origin, Attraction destination)
    {
        // Create a unique cache key based on the two attraction IDs
        string cacheKey = $"DriveTime_{origin.Id}_{destination.Id}";

        // Try to get from cache; if it doesn't exist, call OpenRouteService
        if (!_cache.TryGetValue(cacheKey, out int driveMinutes))
        {
            try
            {
                driveMinutes = await _routeService.GetDrivingMinutesAsync(
                    origin.Latitude, origin.Longitude,
                    destination.Latitude, destination.Longitude
                );

                // Store in cache for 7 days (since static locations rarely change drive times)
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromDays(7));
                
                _cache.Set(cacheKey, driveMinutes, cacheOptions);
            }
            catch
            {
                // Fallback: If ORS API fails or rate limits, assume 15 minutes of driving
                return 15;
            }
        }

        return driveMinutes;
    }

    /// <summary>
    /// Helper to convert strings like "2 hours" or "45 mins" into double hours.
    /// </summary>
    private static double ParseDuration(string durationString)
    {
        if (string.IsNullOrWhiteSpace(durationString)) return 1.0; // Default 1 hour
        
        string lower = durationString.ToLower();
        double multiplier = lower.Contains("min") ? 1.0 / 60.0 : 1.0;
        
        // Extract the first number found in the string
        var match = System.Text.RegularExpressions.Regex.Match(durationString, @"[\d\.]+");
        if (match.Success && double.TryParse(match.Value, out double parsedValue))
        {
            return parsedValue * multiplier;
        }

        return 2.0; // Default 2 hours if parsing fails
    }
}