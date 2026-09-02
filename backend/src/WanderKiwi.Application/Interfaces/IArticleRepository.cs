using WanderKiwi.Domain.Entities;

namespace WanderKiwi.Application.Interfaces;

public interface IArticleRepository
{
    Task<IEnumerable<Article>> GetArticlesAsync(string? category, string? search);
}