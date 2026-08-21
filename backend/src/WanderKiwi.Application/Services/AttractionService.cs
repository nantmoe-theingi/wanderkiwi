using WanderKiwi.Application.DTOs;
using WanderKiwi.Application.Interfaces;

namespace WanderKiwi.Application.Services;

public class AttractionService : IAttractionService
{
    private readonly IAttractionRepository _attractionRepository;

    // Inject the repository through the constructor (Dependency Injection)
    public AttractionService(IAttractionRepository attractionRepository)
    {
        _attractionRepository = attractionRepository;
    }

    public async Task<IEnumerable<AttractionDto>> GetAllAsync()
    {
        var attractions = await _attractionRepository.GetAllAsync();
        return attractions.Select(MapToDto);
    }

    public async Task<IEnumerable<AttractionDto>> SearchAsync(string searchTerm)
{
    var attractions = await _attractionRepository.SearchAsync(searchTerm);

    return attractions.Select(MapToDto);
}

    public async Task<AttractionDto?> GetByIdAsync(int id)
    {
        var attraction = await _attractionRepository.GetByIdAsync(id);
        if (attraction == null) return null;

        return MapToDto(attraction);
    }

    public async Task<AttractionDto> CreateAsync(AttractionDto attractionDto)
    {
        var attraction = MapToEntity(attractionDto);
        var createdAttraction = await _attractionRepository.AddAsync(attraction);
        return MapToDto(createdAttraction);
    }

    public async Task<bool> UpdateAsync(int id, AttractionDto attractionDto)
    {
        var attractionToUpdate = await _attractionRepository.GetByIdAsync(id);
        if (attractionToUpdate == null)
        {
            return false;
        }

        // Map updated fields from DTO to entity

        attractionToUpdate.Id = attractionDto.Id;
        attractionToUpdate.Name = attractionDto.Name;
        attractionToUpdate.Description = attractionDto.Description;
        attractionToUpdate.ImageUrl = attractionDto.ImageUrl;

        attractionToUpdate.Latitude = attractionDto.Latitude;
        attractionToUpdate.Longitude = attractionDto.Longitude;

        attractionToUpdate.Rating = attractionDto.Rating;
        attractionToUpdate.ReviewCount = attractionDto.ReviewCount;

        attractionToUpdate.BestTime = attractionDto.BestTime;
        attractionToUpdate.RecommendedDuration = attractionDto.RecommendedDuration;

        attractionToUpdate.DestinationId = attractionDto.DestinationId;

        await _attractionRepository.UpdateAsync(attractionToUpdate);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var attractionToDelete = await _attractionRepository.GetByIdAsync(id);
        if (attractionToDelete == null)
        {
            return false;
        }

        await _attractionRepository.DeleteAsync(id);
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
        ImageUrl = attraction.ImageUrl,

        Latitude = attraction.Latitude,
        Longitude = attraction.Longitude,

        Rating = attraction.Rating,
        ReviewCount = attraction.ReviewCount,

        BestTime = attraction.BestTime,
        RecommendedDuration = attraction.RecommendedDuration,

        DestinationId = attraction.DestinationId,
        DestinationName = attraction.Destination?.Name ?? string.Empty,

        RegionId = attraction.Destination?.RegionId ?? 0,
        RegionName = attraction.Destination?.Region?.Name ?? string.Empty,

        IslandId = attraction.Destination?.Region?.IslandId ?? 0,
        IslandName = attraction.Destination?.Region?.Island?.Name ?? string.Empty,

        Categories = attraction.AttractionCategories
            .Select(ac => ac.Category.Name)
            .ToList()
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
            ImageUrl = attractionDto.ImageUrl,

            Latitude = attractionDto.Latitude,
            Longitude = attractionDto.Longitude,

            Rating = attractionDto.Rating,
            ReviewCount = attractionDto.ReviewCount,

            BestTime = attractionDto.BestTime,
            RecommendedDuration = attractionDto.RecommendedDuration,

            DestinationId = attractionDto.DestinationId
        };
    }
}