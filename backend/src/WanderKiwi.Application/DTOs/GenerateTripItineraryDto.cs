namespace WanderKiwi.Application.DTOs;


public class GeneratedTripItineraryDto
{
    public string TripName { get; set; } = string.Empty;

    public string DestinationName { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int TotalDays { get; set; }

    public int Travelers { get; set; }

    public string TripStyle { get; set; } = string.Empty;

    public List<string> Interests { get; set; } = [];

    public string Budget { get; set; } = string.Empty;

    public string TransportMode { get; set; } = string.Empty;

    public string Summary { get; set; } = string.Empty;

    public List<GeneratedTripDayDto> Days { get; set; } = [];
}

public class GeneratedTripDayDto
{
    public int DayNumber { get; set; }

    public DateTime Date { get; set; }

    public string Theme { get; set; } = string.Empty;

    public string Summary { get; set; } = string.Empty;

    public List<GeneratedTripStopDto> Stops { get; set; } = [];
}


public class GeneratedTripStopDto
{
    public int Order { get; set; }

    public string Type { get; set; } = string.Empty;

    public string StartTime { get; set; } = string.Empty;

    public string EndTime { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public AttractionReferenceDto? Attraction { get; set; }

    public DrivingInfoDto? Driving { get; set; }

    public int DurationMinutes { get; set; }

    public bool WeatherDependent { get; set; }

    // Database enrichment
    public bool IsFromDatabase { get; set; }

    public int? AttractionId { get; set; }

    public string? ImageUrl { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public string? DataSource { get; set; }
}

public class AttractionReferenceDto
{
    public string Name { get; set; } = string.Empty;

    public string? Location { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }
}

public class DrivingInfoDto
{
    public int DurationMinutes { get; set; }

    public double DistanceKm { get; set; }

    public bool IsRealRoute { get; set; }
}