namespace WanderKiwi.Domain.Entities;

public class Article
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Stores the structured JSON block for the article body
    public string ContentJson { get; set; } = string.Empty;
    
    public string Category { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string AuthorName { get; set; } = string.Empty;
    public string AuthorAvatar { get; set; } = string.Empty;
    public string Date { get; set; } = string.Empty;
    public string ReadTime { get; set; } = string.Empty;
    public int ViewsCount { get; set; }
}