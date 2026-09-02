using Microsoft.EntityFrameworkCore;
using WanderKiwi.Application.Interfaces;
using WanderKiwi.Domain.Entities;
using WanderKiwi.Infrastructure.Data;

namespace WanderKiwi.Infrastructure.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly WanderKiwiDbContext _context;

    public ArticleRepository(WanderKiwiDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Attraction>> GetAllAsync()
    {
        return await _context.Attractions
            .Include(a => a.Destination)
                .ThenInclude(d => d.Region)
                    .ThenInclude(r => r.Island)
            .Include(a => a.AttractionCategories)
                .ThenInclude(ac => ac.Category)
            .ToListAsync();
    }

    public async Task<IEnumerable<Article>> GetArticlesAsync(string? category, string? search)
    {
        IQueryable<Article> query = _context.Articles;

        // Filter by category if provided and not "All Articles"
        if (!string.IsNullOrEmpty(category) && category != "All Articles")
        {
            query = query.Where(a => a.Category.ToLower() == category.ToLower());
        }

        // Filter by search query if provided
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(a => a.Title.ToLower().Contains(search.ToLower()) ||
                                     a.Description.ToLower().Contains(search.ToLower()));
        }

        return await query.ToListAsync();
    }
}