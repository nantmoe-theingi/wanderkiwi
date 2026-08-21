namespace WanderKiwi.Application.DTOs;

public class DestinationDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public decimal Rating { get; set; }

    public int ReviewCount { get; set; }

    public bool IsPopular { get; set; }

    // Region information
    public int RegionId { get; set; }

    public string RegionName { get; set; } = string.Empty;

    // Island information
    public int IslandId { get; set; }

    public string IslandName { get; set; } = string.Empty;

    // Categories
    public List<string> Categories { get; set; } = new();

    // Attractions
    public List<AttractionSummaryDto> Attractions { get; set; } = new();
}