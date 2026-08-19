using WanderKiwi.Domain.Entities;

namespace WanderKiwi.Services.Interfaces;

public interface IAttractionRepository
{
    // Fetches all attractions from the database
    Task<IEnumerable<Attraction>> GetAllAsync();

    // Searches the database based on a search term or region
    Task<IEnumerable<Attraction>> SearchAsync(string? searchTerm, string? region);

    // Fetches a single attraction by its ID
    Task<Attraction?> GetByIdAsync(int id);

    // Adds a new attraction to the database
    Task<Attraction> AddAsync(Attraction attraction);

    // Updates an existing attraction in the database
    Task UpdateAsync(Attraction attraction);

    // Deletes an attraction from the database
    Task DeleteAsync(int id);
}