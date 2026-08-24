namespace WanderKiwi.Domain.Entities;

public class Island
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public ICollection<Region> Regions { get; set; } = new List<Region>();
}