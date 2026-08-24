namespace WanderKiwi.Domain.Entities;

public class TripStop
{
    public int Id { get; set; }

    public int TripDayId { get; set; }

    public TripDay TripDay { get; set; } = null!;

    // Stops can either point to a known attraction or be a custom location.
    public int? AttractionId { get; set; }

    public Attraction? Attraction { get; set; }

    public string? CustomName { get; set; }

    public int SortOrder { get; set; }

    public string? Notes { get; set; }

    public int? PlannedDurationMinutes { get; set; }
}
