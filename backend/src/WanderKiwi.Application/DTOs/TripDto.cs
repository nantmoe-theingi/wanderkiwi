namespace WanderKiwi.Application.DTOs;

public class TripDto
{
    public int Id { get; set; }
    public string OwnerId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string BudgetRange { get; set; } = string.Empty;
    public string TripStyle { get; set; } = string.Empty;
    public List<TripDayDto> Days { get; set; } = new();
}

public class TripDayDto
{
    public int Id { get; set; }
    public int DayNumber { get; set; }
    public DateOnly Date { get; set; }
    public List<TripStopDto> Stops { get; set; } = new();
}

public class TripStopDto
{
    public int Id { get; set; }
    public int? AttractionId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public int SortOrder { get; set; }
    public int? PlannedDurationMinutes { get; set; }
    public string? Notes { get; set; }
}
