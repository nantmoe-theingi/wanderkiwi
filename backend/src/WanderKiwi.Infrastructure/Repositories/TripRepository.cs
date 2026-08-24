using Microsoft.EntityFrameworkCore;
using WanderKiwi.Application.Interfaces;
using WanderKiwi.Domain.Entities;
using WanderKiwi.Infrastructure.Data;

namespace WanderKiwi.Infrastructure.Repositories;

public class TripRepository : ITripRepository
{
    private readonly WanderKiwiDbContext _context;

    public TripRepository(WanderKiwiDbContext context)
    {
        _context = context;
    }

    public Task<Trip?> GetByIdAsync(int id) =>
        IncludeTripDetails(_context.Trips).FirstOrDefaultAsync(t => t.Id == id);

    public Task<List<Trip>> GetByOwnerIdAsync(string ownerId) =>
        IncludeTripDetails(_context.Trips)
            .Where(t => t.OwnerId == ownerId)
            .OrderByDescending(t => t.StartDate)
            .ToListAsync();

    public async Task<Trip> AddAsync(Trip trip)
    {
        _context.Trips.Add(trip);
        await _context.SaveChangesAsync();
        return trip;
    }

    public async Task UpdateAsync(Trip trip)
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Trip trip)
    {
        _context.Trips.Remove(trip);
        await _context.SaveChangesAsync();
    }

    public Task<bool> AttractionExistsAsync(int attractionId) =>
        _context.Attractions.AnyAsync(a => a.Id == attractionId);

    private static IQueryable<Trip> IncludeTripDetails(IQueryable<Trip> query) =>
        query
            .Include(t => t.Days)
                .ThenInclude(d => d.Stops)
                    .ThenInclude(s => s.Attraction);
}
