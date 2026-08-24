using WanderKiwi.Application.DTOs;

namespace WanderKiwi.Application.Interfaces;

public interface ITripService
{
    Task<TripDto> CreateAsync(CreateTripDto request);
    Task<TripDto?> GetByIdAsync(int id);
    Task<IEnumerable<TripDto>> GetByOwnerIdAsync(string ownerId);
    Task<bool> UpdateAsync(int id, UpdateTripDto request);
    Task<bool> DeleteAsync(int id);
    Task<TripStopDto?> AddStopAsync(int tripId, int dayNumber, AddTripStopDto request);
}
