namespace WanderKiwi.Domain.Entities;

public class DestinationLookup
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string RegionName { get; set; } = string.Empty;

    public int IslandId { get; set; }
    public string IslandName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public decimal Rating { get; set; }
    public List<string> Categories { get; set; } = new();
}