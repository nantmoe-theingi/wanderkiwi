using WanderKiwi.Application.DTOs;

namespace WanderKiwi.Application.Interfaces;

public interface IAttractionService
{
    Task<IEnumerable<AttractionDto>> GetAllAsync();

    Task<AttractionDto?> GetByIdAsync(int id);

    Task<AttractionDto> CreateAsync(AttractionDto attractionDto);

    Task<bool> UpdateAsync(int id, AttractionDto attractionDto);

    Task<bool> DeleteAsync(int id);

    Task<IEnumerable<AttractionDto>> SearchAsync(string searchTerm);
}