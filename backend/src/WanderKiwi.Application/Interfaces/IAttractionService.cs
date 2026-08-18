using WanderKiwi.Application.DTOs;

namespace WanderKiwi.Application.Interfaces;

public interface IAttractionService
{
    // Retrieves all attractions
    Task<IEnumerable<AttractionDto>> GetAllAttractionsAsync();

    // Supports the search and filter features of the Destination Explorer
    Task<IEnumerable<AttractionDto>> SearchAttractionsAsync(string? searchTerm, string? region);

    // Retrieves details for a specific attraction
    Task<AttractionDto?> GetAttractionByIdAsync(int id);

    // Creates a new attraction
    Task<AttractionDto> CreateAttractionAsync(AttractionDto attraction);

    // Updates an existing attraction
    Task<bool> UpdateAttractionAsync(int id, AttractionDto attraction);

    // Deletes an attraction
    Task<bool> DeleteAttractionAsync(int id);
}