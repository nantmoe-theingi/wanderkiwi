using Microsoft.EntityFrameworkCore;
using WanderKiwi.Domain.Entities;
using WanderKiwi.Infrastructure.Data;
using WanderKiwi.Application.Interfaces;

namespace WanderKiwi.Infrastructure.Repositories;

public class AttractionRepository : IAttractionRepository
{
    private readonly WanderKiwiDbContext _context;

    public AttractionRepository(WanderKiwiDbContext context)
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

    public async Task<Attraction?> GetByIdAsync(int id)
    {
        return await _context.Attractions
            .Include(a => a.Destination)
                .ThenInclude(d => d.Region)
                    .ThenInclude(r => r.Island)
            .Include(a => a.AttractionCategories)
                .ThenInclude(ac => ac.Category)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Attraction> AddAsync(Attraction attraction)
    {
        _context.Attractions.Add(attraction);
        await _context.SaveChangesAsync();

        return attraction;
    }

    public async Task UpdateAsync(Attraction attraction)
    {
        _context.Attractions.Update(attraction);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var attraction = await _context.Attractions
            .FirstOrDefaultAsync(a => a.Id == id);

        if (attraction != null)
        {
            _context.Attractions.Remove(attraction);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Attraction>> SearchAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return Enumerable.Empty<Attraction>();
        }

        searchTerm = searchTerm.Trim();

        return await _context.Attractions
            .Include(a => a.Destination)
                .ThenInclude(d => d.Region)
                    .ThenInclude(r => r.Island)
            .Include(a => a.AttractionCategories)
                .ThenInclude(ac => ac.Category)
            .Where(a =>
                a.Name.Contains(searchTerm) ||
                a.Description.Contains(searchTerm) ||
                a.Destination.Name.Contains(searchTerm) ||
                a.Destination.Region.Name.Contains(searchTerm) ||
                a.Destination.Region.Island.Name.Contains(searchTerm) ||
                a.AttractionCategories.Any(ac => ac.Category.Name.Contains(searchTerm)))
            .ToListAsync();
    }

    public Task<List<Attraction>> GetByDestinationAsync(string destinationName) =>
        _context.Attractions
            .AsNoTracking()
            .Include(a => a.AttractionCategories)
                .ThenInclude(ac => ac.Category)
            .Where(a => a.Destination.Name == destinationName)
            .OrderByDescending(a => a.Rating)
            .ToListAsync();
}
