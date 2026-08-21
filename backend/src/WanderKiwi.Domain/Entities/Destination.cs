namespace WanderKiwi.Domain.Entities;

public class Destination
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public int RegionId { get; set; }

    public Region Region { get; set; } = null!;

    public decimal Rating { get; set; }

    public int ReviewCount { get; set; }

    public bool IsPopular { get; set; }

    public ICollection<DestinationCategory> DestinationCategories { get; set; }
        = new List<DestinationCategory>();

    public ICollection<Attraction> Attractions { get; set; }
        = new List<Attraction>();
}