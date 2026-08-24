namespace WanderKiwi.Application.DTOs;

public class RegionDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int IslandId { get; set; }

    public string IslandName { get; set; } = string.Empty;
}