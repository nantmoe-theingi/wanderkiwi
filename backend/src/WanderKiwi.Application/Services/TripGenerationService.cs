using WanderKiwi.Application.DTOs;
using WanderKiwi.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;

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
            var currentDate = request.StartDate.AddDays(dayIndex);
            var dayDto = new TripDayItineraryDto
            {
                DayNumber = dayIndex + 1,
                Date = currentDate,
                Theme = GetDayTheme(dayIndex, request.TripStyle),
                Stops = new List<TripStopItineraryDto>()
            };

            // Limit to max 3 attractions per day
            int stopsForToday = Math.Min(3, availableQueue.Count);
            var todayAttractions = availableQueue.Take(stopsForToday).ToList();

            // Remove assigned spots from available queue
            availableQueue.RemoveRange(0, stopsForToday);

            string[] defaultTimeSlots = { "09:30 AM - 11:30 AM", "01:00 PM - 03:30 PM", "04:00 PM - 06:30 PM" };

            for (int i = 0; i < todayAttractions.Count; i++)
            {
                var attr = todayAttractions[i];
                dayDto.Stops.Add(new TripStopItineraryDto
                {
                    Order = i + 1,
                    AttractionId = attr.Id,
                    AttractionName = attr.Name,
                    ImageUrl = attr.ImageUrl,
                    Description = attr.Description,
                    RecommendedDuration = attr.RecommendedDuration,
                    BestTime = attr.BestTime,
                    TimeSlot = defaultTimeSlots[Math.Min(i, defaultTimeSlots.Length - 1)],
                    OpeningHoursNote = attr.OpeningHoursNote,
                    BookingNote = attr.BookingNote,
                    AvailabilityNote = attr.AvailabilityNote,
                    Latitude = attr.Latitude,
                    Longitude = attr.Longitude
                });
            }

            response.Days.Add(dayDto);

            // Re-fill queue from full pool if multi-day itinerary runs out of unassigned spots
            if (!availableQueue.Any() && candidateAttractions.Any())
            {
                availableQueue = candidateAttractions.OrderBy(a => Guid.NewGuid()).ToList();
            }
        }

        return response;
    }

    private static string GetDayTheme(int dayIndex, string tripStyle)
    {
        return dayIndex switch
        {
            0 => $"{tripStyle} Highlights & Scenic Views",
            1 => "Local Exploration & Hidden Gems",
            2 => "Outdoor Excursions & Nature Walk",
            _ => $"Day {dayIndex + 1} Exploration"
        };
    }
}