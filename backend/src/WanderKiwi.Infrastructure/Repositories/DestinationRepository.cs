using Microsoft.EntityFrameworkCore;
using WanderKiwi.Application.Interfaces;
using WanderKiwi.Application.Models;
using WanderKiwi.Domain.Entities;
using WanderKiwi.Infrastructure.Data;

namespace WanderKiwi.Infrastructure.Repositories;

public class DestinationRepository : IDestinationRepository
{
    private readonly WanderKiwiDbContext _context;

    public DestinationRepository(WanderKiwiDbContext context)
    {
        _context = context;
    }

    public async Task<DestinationPageData> GetDestinationPageAsync()
    {
        var popularDestinations = await _context.Destinations
            .AsNoTracking()
            .Include(d => d.Region)
                .ThenInclude(r => r.Island)
            .Include(d => d.DestinationCategories)
                .ThenInclude(dc => dc.Category)
            .Where(d => d.IsPopular)
            .OrderByDescending(d => d.Rating)
            .Take(8)
            .ToListAsync();

        var regions = await _context.Regions
            .AsNoTracking()
            .Include(r => r.Island)
            .OrderBy(r => r.IslandId)
            .ThenBy(r => r.Name)
            .ToListAsync();

        var featuredAttractions = await _context.Attractions
            .AsNoTracking()
            .Include(a => a.Destination)
                .ThenInclude(d => d.Region)
                    .ThenInclude(r => r.Island)
            .Include(a => a.AttractionCategories)
                .ThenInclude(ac => ac.Category)
            .OrderByDescending(a => a.Rating)
            .Take(8)
            .ToListAsync();

        return new DestinationPageData
        {
            PopularDestinations = popularDestinations,
            Regions = regions,
            FeaturedAttractions = featuredAttractions
        };
    }

    public async Task<Destination> GetByIdAsync(int id)
    {
        return await _context.Destinations
                .Include(a => a.Region)
                    .ThenInclude(r => r.Island)
            .Include(a => a.DestinationCategories)
                .ThenInclude(ac => ac.Category)
            .FirstAsync(a => a.Id == id);
    }
}