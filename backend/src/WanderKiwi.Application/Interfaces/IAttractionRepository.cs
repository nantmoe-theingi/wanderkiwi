using WanderKiwi.Domain.Entities;

namespace WanderKiwi.Application.Interfaces;

public interface IAttractionRepository
{
    Task<IEnumerable<Attraction>> GetAllAsync();

    Task<Attraction> GetByIdAsync(int id);

    Task<Attraction> AddAsync(Attraction attraction);

    Task UpdateAsync(Attraction attraction);

    Task DeleteAsync(int id);

    Task<IEnumerable<Attraction>> SearchAsync(string searchTerm);

    Task<List<Attraction>> GetByDestinationIdAsync(int destinationId);

    Task<List<Attraction>> GetByDestinationAsync(string destinationName);
}
