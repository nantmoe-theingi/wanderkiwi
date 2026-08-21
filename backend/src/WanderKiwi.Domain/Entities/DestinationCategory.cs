namespace WanderKiwi.Domain.Entities;

public class DestinationCategory
{
    public int DestinationId { get; set; }

    public Destination Destination { get; set; } = null!;

    public int CategoryId { get; set; }

    public Category Category { get; set; } = null!;
}