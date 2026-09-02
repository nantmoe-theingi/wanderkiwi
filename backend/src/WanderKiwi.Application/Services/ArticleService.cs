using WanderKiwi.Application.Interfaces;
using WanderKiwi.Domain.Entities;

namespace WanderKiwi.Application.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _articleRepository;

    public ArticleService(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public async Task<IEnumerable<Article>> SearchAsync(string? category, string? search)
    {
        return await _articleRepository.GetArticlesAsync(category, search);
    }

    public async Task<Article?> GetByIdAsync(int id)
    {
        var articles = await _articleRepository.GetArticlesAsync(null, null);
        return articles.FirstOrDefault(a => a.Id == id);
    }
}