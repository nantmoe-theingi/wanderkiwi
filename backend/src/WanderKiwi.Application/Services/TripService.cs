using WanderKiwi.Application.DTOs;
using WanderKiwi.Application.Interfaces;
using WanderKiwi.Domain.Entities;

namespace WanderKiwi.Application.Services;

public class TripService : ITripService
{
    private readonly ITripRepository _tripRepository;
    private readonly IAttractionRepository? _attractionRepository;
    private readonly IRouteService? _routeService;

    public TripService(ITripRepository tripRepository)
    {
        _tripRepository = tripRepository;
    }

    public TripService(ITripRepository tripRepository, IAttractionRepository attractionRepository,
        IRouteService routeService) : this(tripRepository)
    {
        _attractionRepository = attractionRepository;
        _routeService = routeService;
    }

    public async Task<TripDto> CreateAsync(CreateTripDto request)
    {
        ValidateDates(request.StartDate, request.EndDate);

        var trip = new Trip
        {
            OwnerId = request.OwnerId.Trim(),
            Name = request.Name.Trim(),
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            BudgetRange = request.BudgetRange.Trim(),
            TripStyle = request.TripStyle.Trim()
        };

        for (var date = request.StartDate; date <= request.EndDate; date = date.AddDays(1))
        {
            trip.Days.Add(new TripDay
            {
                DayNumber = trip.Days.Count + 1,
                Date = date
            });
        }

        if (_attractionRepository is not null && _routeService is not null)
        {
            await GenerateFixedItineraryAsync(trip, request.Interests);
        }

        await _tripRepository.AddAsync(trip);
        return MapToDto(trip);
    }

    public async Task<TripDto?> GetByIdAsync(int id)
    {
        var trip = await _tripRepository.GetByIdAsync(id);
        return trip is null ? null : MapToDto(trip);
    }

    public async Task<IEnumerable<TripDto>> GetByOwnerIdAsync(string ownerId)
    {
        var trips = await _tripRepository.GetByOwnerIdAsync(ownerId.Trim());
        return trips.Select(MapToDto);
    }

    public async Task<bool> UpdateAsync(int id, UpdateTripDto request)
    {
        ValidateDates(request.StartDate, request.EndDate);
        var trip = await _tripRepository.GetByIdAsync(id);
        if (trip is null)
        {
            return false;
        }

        var existingStops = trip.Days.SelectMany(d => d.Stops).Any();
        var dayCountChanged = trip.StartDate != request.StartDate || trip.EndDate != request.EndDate;
        if (dayCountChanged && existingStops)
        {
            throw new InvalidOperationException("Dates cannot be changed after stops have been added. Remove the stops first.");
        }

        trip.Name = request.Name.Trim();
        trip.StartDate = request.StartDate;
        trip.EndDate = request.EndDate;
        trip.BudgetRange = request.BudgetRange.Trim();
        trip.TripStyle = request.TripStyle.Trim();

        if (dayCountChanged)
        {
            trip.Days.Clear();
            for (var date = request.StartDate; date <= request.EndDate; date = date.AddDays(1))
            {
                trip.Days.Add(new TripDay { DayNumber = trip.Days.Count + 1, Date = date });
            }
        }

        await _tripRepository.UpdateAsync(trip);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var trip = await _tripRepository.GetByIdAsync(id);
        if (trip is null)
        {
            return false;
        }

        await _tripRepository.DeleteAsync(trip);
        return true;
    }

    public async Task<TripStopDto?> AddStopAsync(int tripId, int dayNumber, AddTripStopDto request)
    {
        if (request.AttractionId is null && string.IsNullOrWhiteSpace(request.CustomName))
        {
            throw new ArgumentException("Choose an attraction or provide a custom stop name.");
        }

        if (request.AttractionId is not null && !await _tripRepository.AttractionExistsAsync(request.AttractionId.Value))
        {
            throw new ArgumentException("The selected attraction does not exist.");
        }

        var trip = await _tripRepository.GetByIdAsync(tripId);
        var day = trip?.Days.SingleOrDefault(d => d.DayNumber == dayNumber);
        if (day is null)
        {
            return null;
        }

        var stop = new TripStop
        {
            AttractionId = request.AttractionId,
            CustomName = string.IsNullOrWhiteSpace(request.CustomName) ? null : request.CustomName.Trim(),
            Notes = string.IsNullOrWhiteSpace(request.Notes) ? null : request.Notes.Trim(),
            PlannedDurationMinutes = request.PlannedDurationMinutes,
            SortOrder = day.Stops.Count
        };
        day.Stops.Add(stop);

        await _tripRepository.UpdateAsync(trip!);
        return MapStopToDto(stop);
    }

    private static void ValidateDates(DateOnly startDate, DateOnly endDate)
    {
        if (startDate == default || endDate == default || endDate < startDate)
        {
            throw new ArgumentException("End date must be on or after the start date.");
        }
    }

    private async Task GenerateFixedItineraryAsync(Trip trip, List<string> interests)
    {
        var destinationName = NormalizeDestinationName(trip.Name);
        var attractions = await _attractionRepository!.GetByDestinationAsync(destinationName);
        if (attractions.Count < trip.Days.Count)
        {
            throw new ArgumentException($"We do not yet have enough verified attractions to build a {trip.Days.Count}-day plan for {trip.Name}.");
        }

        var preferred = attractions
            .Where(a => interests.Count == 0 || a.AttractionCategories.Any(c => interests.Contains(c.Category.Name)))
            .OrderByDescending(a => a.Rating)
            .Concat(attractions.OrderByDescending(a => a.Rating))
            .DistinctBy(a => a.Id)
            .Take(trip.Days.Count)
            .ToList();

        // The initial supported destination is Queenstown, where trips begin and end in town centre.
        // Additional destinations will receive their own verified accommodation/base coordinates.
        var baseLocation = GetBaseLocation(destinationName);

        var generatedDays = 0;

        foreach (var attraction in preferred)
        {
            if (generatedDays == trip.Days.Count)
            {
                break;
            }

            try
            {
                var oneWayMinutes = await _routeService!.GetDrivingMinutesAsync(
                    baseLocation.Latitude,
                    baseLocation.Longitude,
                    attraction.Latitude,
                    attraction.Longitude);

                var visitMinutes = ParseRecommendedDurationMinutes(
                    attraction.RecommendedDuration);

                trip.Days.ElementAt(generatedDays).Stops.Add(new TripStop
                {
                    AttractionId = attraction.Id,
                    CustomName = attraction.Name,
                    SortOrder = 0,
                    PlannedDurationMinutes = visitMinutes,
                    Notes =
                        $"Allow about {oneWayMinutes} minutes to travel from " +
                        $"{destinationName} town centre, plus " +
                        $"{visitMinutes / 60.0:0.#} hours to visit."
                });

                generatedDays++;
            }
            catch (InvalidOperationException)
            {
                // The stored point is not reachable by road.
                // Skip it and try the next verified attraction.
            }
        }

        if (generatedDays < trip.Days.Count)
        {
            throw new ArgumentException(
                $"We could only create {generatedDays} routable days for " +
                $"{destinationName}. Add more road-accessible attractions.");
        }
    }

    private static (double Latitude, double Longitude) GetBaseLocation(string destinationName) =>
        destinationName.Trim().ToLowerInvariant() switch
        {
            "queenstown" or "queenstown, new zealand" => (-45.0312, 168.6626),
            "christchurch" => (-43.5321, 172.6362),
            "auckland" => (-36.8485, 174.7633),
            _ => throw new ArgumentException($"{destinationName} is not yet a supported generated-plan location.")
        };

    private static string NormalizeDestinationName(string destinationName) =>
        destinationName.Trim().Replace(", New Zealand", string.Empty, StringComparison.OrdinalIgnoreCase).Trim();

    private static int ParseRecommendedDurationMinutes(string recommendedDuration)
    {
        var firstNumber = new string(recommendedDuration
            .SkipWhile(character => !char.IsDigit(character))
            .TakeWhile(char.IsDigit)
            .ToArray());
        if (!int.TryParse(firstNumber, out var amount))
        {
            return 120;
        }

        return recommendedDuration.Contains("day", StringComparison.OrdinalIgnoreCase) ? amount * 8 * 60 : amount * 60;
    }

    private static TripDto MapToDto(Trip trip) => new()
    {
        Id = trip.Id,
        OwnerId = trip.OwnerId,
        Name = trip.Name,
        StartDate = trip.StartDate,
        EndDate = trip.EndDate,
        BudgetRange = trip.BudgetRange,
        TripStyle = trip.TripStyle,
        Days = trip.Days.OrderBy(d => d.DayNumber).Select(day => new TripDayDto
        {
            Id = day.Id,
            DayNumber = day.DayNumber,
            Date = day.Date,
            Stops = day.Stops.OrderBy(stop => stop.SortOrder).Select(MapStopToDto).ToList()
        }).ToList()
    };

    private static TripStopDto MapStopToDto(TripStop stop) => new()
    {
        Id = stop.Id,
        AttractionId = stop.AttractionId,
        Name = stop.Attraction?.Name ?? stop.CustomName ?? string.Empty,
        ImageUrl = stop.Attraction?.ImageUrl,
        SortOrder = stop.SortOrder,
        PlannedDurationMinutes = stop.PlannedDurationMinutes,
        Notes = stop.Notes
    };
}
