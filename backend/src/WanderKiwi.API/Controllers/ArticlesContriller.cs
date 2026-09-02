using Microsoft.AspNetCore.Mvc;
using WanderKiwi.Domain.Entities;
using WanderKiwi.Application.Interfaces;

namespace WanderKiwi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticlesController : ControllerBase
{
    private readonly IArticleService _articleService;

    // Inject the Application Service
    public ArticlesController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    // GET: api/Articles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Article>>> GetArticles(
        [FromQuery] string? category,
        [FromQuery] string? search)
    {
        var articles = await _articleService.SearchAsync(category, search);
        return Ok(articles);
    }

    // GET: api/Articles/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Article>> GetArticleById(int id)
    {
        // var article = await _context.Articles.FindAsync(id);

        var article = await _articleService.GetByIdAsync(id);

        if (article == null)
        {
            return NotFound();
        }

        return Ok(article);
    }

    // GET: api/Articles/popular
    // [HttpGet("popular")]
    // public async Task<ActionResult<IEnumerable<Article>>> GetPopularArticles()
    // {
    //     var popular = await _context.Articles
    //         .OrderByDescending(a => a.ViewsCount)
    //         .Take(3) // Gets top 3 popular reads
    //         .ToListAsync();

    //     return Ok(popular);
    // }
}