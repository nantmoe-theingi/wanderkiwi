using Microsoft.EntityFrameworkCore;
using WanderKiwi.Domain.Entities;
using WanderKiwi.Infrastructure.Data;
using WanderKiwi.Services.Interfaces;

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
        return await _context.Attractions.ToListAsync();
    }

    public async Task<IEnumerable<Attraction>> SearchAsync(string? searchTerm, string? region)
    {
        var query = _context.Attractions.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(a => a.Name.Contains(searchTerm) || a.Description.Contains(searchTerm));
        }

        if (!string.IsNullOrWhiteSpace(region))
        {
            query = query.Where(a => a.Region == region);
        }

        return await query.ToListAsync();
    }

    public async Task<Attraction?> GetByIdAsync(int id)
    {
        return await _context.Attractions.FindAsync(id);
    }

    public async Task<Attraction> AddAsync(Attraction attraction)
    {
        _context.Attractions.Add(attraction);
        await _context.SaveChangesAsync();
        return attraction;
    }

    public async Task UpdateAsync(Attraction attraction)
    {
        _context.Entry(attraction).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var attraction = await _context.Attractions.FindAsync(id);
        if (attraction != null)
        {
            _context.Attractions.Remove(attraction);
            await _context.SaveChangesAsync();
        }
    }
}