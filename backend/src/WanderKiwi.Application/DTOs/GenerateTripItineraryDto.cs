namespace WanderKiwi.Application.DTOs;

public class GeneratedTripItineraryDto
{
    public string TripName { get; set; } = string.Empty;
    public string DestinationName { get; set; } = string.Empty;
    public int TotalDays { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<TripDayItineraryDto> Days { get; set; } = new();
}

public class TripDayItineraryDto
{
    public int DayNumber { get; set; }
    public DateTime Date { get; set; }
    public string Theme { get; set; } = string.Empty;
    public List<TripStopItineraryDto> Stops { get; set; } = new();
}

public class TripStopItineraryDto
{
    public int Order { get; set; }
    public int AttractionId { get; set; }
    public string AttractionName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string RecommendedDuration { get; set; } = string.Empty;
    public string BestTime { get; set; } = string.Empty;
    public string TimeSlot { get; set; } = string.Empty; // e.g. "09:30 AM - 11:30 AM"
    public string OpeningHoursNote { get; set; } = string.Empty;
    public string BookingNote { get; set; } = string.Empty;
    public string AvailabilityNote { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int DriveTimeToNextMinutes { get; set; }
}