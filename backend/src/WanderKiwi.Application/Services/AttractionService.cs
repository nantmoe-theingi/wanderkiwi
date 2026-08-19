using WanderKiwi.Application.DTOs;
using WanderKiwi.Application.Interfaces;
using WanderKiwi.Services.Interfaces;

namespace WanderKiwi.Application.Services;

public class AttractionService : IAttractionService
{
    private readonly IAttractionRepository _repository;

    // Inject the repository through the constructor (Dependency Injection)
    public AttractionService(IAttractionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AttractionDto>> GetAllAttractionsAsync()
    {
        var attractions = await _repository.GetAllAsync();
        return attractions.Select(MapToDto);
    }

    public async Task<IEnumerable<AttractionDto>> SearchAttractionsAsync(string? searchTerm, string? region)
    {
        var attractions = await _repository.SearchAsync(searchTerm, region);
        return attractions.Select(MapToDto);
    }

    public async Task<AttractionDto?> GetAttractionByIdAsync(int id)
    {
        var attraction = await _repository.GetByIdAsync(id);
        if (attraction == null) return null;

        return MapToDto(attraction);
    }

    public async Task<AttractionDto> CreateAttractionAsync(AttractionDto attractionDto)
    {
        var attraction = MapToEntity(attractionDto);
        var createdAttraction = await _repository.AddAsync(attraction);
        return MapToDto(createdAttraction);
    }

    public async Task<bool> UpdateAttractionAsync(int id, AttractionDto attractionDto)
    {
        var attractionToUpdate = await _repository.GetByIdAsync(id);
        if (attractionToUpdate == null)
        {
            return false;
        }

        // Map updated fields from DTO to entity
        attractionToUpdate.Name = attractionDto.Name;
        attractionToUpdate.Description = attractionDto.Description;
        attractionToUpdate.Region = attractionDto.Region;
        attractionToUpdate.Latitude = attractionDto.Latitude;
        attractionToUpdate.Longitude = attractionDto.Longitude;
        attractionToUpdate.ImageUrl = attractionDto.ImageUrl;

        await _repository.UpdateAsync(attractionToUpdate);
        return true;
    }

    public async Task<bool> DeleteAttractionAsync(int id)
    {
        var attractionToDelete = await _repository.GetByIdAsync(id);
        if (attractionToDelete == null)
        {
            return false;
        }

        await _repository.DeleteAsync(id);
        return true;
    }

    // Helper method to map the Domain Entity to the Application DTO
    private AttractionDto MapToDto(Domain.Entities.Attraction attraction)
    {
        return new AttractionDto
        {
            Id = attraction.Id,
            Name = attraction.Name,
            Description = attraction.Description,
            Region = attraction.Region,
            Latitude = attraction.Latitude,
            Longitude = attraction.Longitude,
            ImageUrl = attraction.ImageUrl
        };
    }

    // Helper method to map the Application DTO to the Domain Entity
    private Domain.Entities.Attraction MapToEntity(AttractionDto attractionDto)
    {
        return new Domain.Entities.Attraction
        {
            Id = attractionDto.Id,
            Name = attractionDto.Name,
            Description = attractionDto.Description,
            Region = attractionDto.Region,
            Latitude = attractionDto.Latitude,
            Longitude = attractionDto.Longitude,
            ImageUrl = attractionDto.ImageUrl
        };
    }
}