namespace WanderKiwi.Application.DTOs;

public class AttractionSummaryDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;

    public string BestTime { get; set; } = string.Empty;

    public decimal Rating { get; set; }
}