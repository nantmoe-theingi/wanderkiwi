namespace WanderKiwi.Domain.Entities;

public class Attraction
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public int DestinationId { get; set; }

    public Destination Destination { get; set; } = null!;

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public decimal Rating { get; set; }

    public int ReviewCount { get; set; }

    public string BestTime { get; set; } = string.Empty;

    public string RecommendedDuration { get; set; } = string.Empty;

    public ICollection<AttractionCategory> AttractionCategories { get; set; }
        = new List<AttractionCategory>();
}