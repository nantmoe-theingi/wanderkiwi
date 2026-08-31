using Microsoft.EntityFrameworkCore;
using WanderKiwi.Application.Interfaces;
using WanderKiwi.Application.Models;
using WanderKiwi.Domain.DTOs;
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

    public async Task<IEnumerable<DestinationLookupDto>> GetDestinationNamesAsync()
    {
        return await _context.Destinations
            .Include(d => d.Region)
            .Select(d => new DestinationLookupDto
            {
                Id = d.Id,
                Name = d.Name,
                RegionName = d.Region != null ? d.Region.Name : string.Empty
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<DestinationLookup>> GetPopularDestinationsAsync()
    {
        var popularDestinations = await _context.Destinations
            .AsNoTracking()
            .Include(d => d.Region)
                .ThenInclude(r => r.Island)
            .Include(d => d.DestinationCategories)
                .ThenInclude(dc => dc.Category)
            .Where(d => d.IsPopular)
            .OrderByDescending(d => d.Rating)
            .Take(4)
            .ToListAsync();

        return popularDestinations.Select(d => new DestinationLookup
        {
            Id = d.Id,
            Name = d.Name,
            RegionName = d.Region?.Name ?? string.Empty,
            IslandId = d.Region?.IslandId ?? 0,
            IslandName = d.Region?.Island?.Name ?? string.Empty,
            Description = d.Description,
            ImageUrl = d.ImageUrl,
            Rating = d.Rating,
            Categories = d.DestinationCategories.Select(dc => dc.Category.Name).ToList()
        });
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

    public async Task<Destination> GetbyNameAsync(string name)
    {
        return await _context.Destinations
                .Include(a => a.Region)
                    .ThenInclude(r => r.Island)
            .Include(a => a.DestinationCategories)
                .ThenInclude(ac => ac.Category)
            .FirstAsync(a => a.Name == name);
    }
}