namespace WanderKiwi.Application.DTOs;

public class AttractionDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public decimal Rating { get; set; }
    public int ReviewCount { get; set; }

    public string BestTime { get; set; } = string.Empty;
    public string RecommendedDuration { get; set; } = string.Empty;

    public string AvailabilityNote { get; set; } = string.Empty;
    public string OpeningHoursNote { get; set; } = string.Empty;
    public string BookingNote { get; set; } = string.Empty;
    public string SourceUrl { get; set; } = string.Empty;

    public int DestinationId { get; set; }
    public string DestinationName { get; set; } = string.Empty;

    public int RegionId { get; set; }
    public string RegionName { get; set; } = string.Empty;

    public int IslandId { get; set; }
    public string IslandName { get; set; } = string.Empty;

    public List<string> Categories { get; set; } = new();
}