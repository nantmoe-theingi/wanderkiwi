namespace WanderKiwi.Domain.Entities;

public class Region
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int IslandId { get; set; }

    public Island Island { get; set; } = null!;

    public string Description { get; set; } = string.Empty;

    public ICollection<Destination> Destinations { get; set; } = new List<Destination>();
}