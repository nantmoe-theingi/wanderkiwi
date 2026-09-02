using WanderKiwi.Domain.Entities;

namespace WanderKiwi.Application.Interfaces;

public interface IArticleService
{
    Task<IEnumerable<Article>> SearchAsync(string? category, string? search);

    Task<Article?> GetByIdAsync(int id);
}