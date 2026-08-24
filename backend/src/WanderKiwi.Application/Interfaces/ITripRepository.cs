using WanderKiwi.Domain.Entities;

namespace WanderKiwi.Application.Interfaces;

public interface ITripRepository
{
    Task<Trip?> GetByIdAsync(int id);
    Task<List<Trip>> GetByOwnerIdAsync(string ownerId);
    Task<Trip> AddAsync(Trip trip);
    Task UpdateAsync(Trip trip);
    Task DeleteAsync(Trip trip);
    Task<bool> AttractionExistsAsync(int attractionId);
}
