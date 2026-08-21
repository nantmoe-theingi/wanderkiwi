namespace WanderKiwi.Domain.Entities;

public class AttractionCategory
{
    public int AttractionId { get; set; }

    public Attraction Attraction { get; set; } = null!;

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;
}