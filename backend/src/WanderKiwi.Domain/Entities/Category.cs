namespace WanderKiwi.Domain.Entities;

public class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<DestinationCategory> DestinationCategories { get; set; }
        = new List<DestinationCategory>();

    public ICollection<AttractionCategory> AttractionCategories { get; set; }
        = new List<AttractionCategory>();
}