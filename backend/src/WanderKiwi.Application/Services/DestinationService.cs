using WandarKiwi.Application.Interfaces;
using WanderKiwi.Application.DTOs;
using WanderKiwi.Application.Interfaces;
using WanderKiwi.Domain.DTOs;
using WanderKiwi.Domain.Entities;

namespace WanderKiwi.Application.Services;

public class DestinationService : IDestinationService
{
    private readonly IDestinationRepository _destinationRepository;

    public DestinationService(IDestinationRepository destinationRepository)
    {
        _destinationRepository = destinationRepository;
    }

    public async Task<DestinationPageDto> GetDestinationPageAsync()
    {
        var pageData =
            await _destinationRepository.GetDestinationPageAsync();

        return new DestinationPageDto
        {
            PopularDestinations = pageData.PopularDestinations
                .Select(MapToDto)
                .ToList(),

            Regions = pageData.Regions
                .Select(MapRegionToDto)
                .ToList(),

            FeaturedAttractions = pageData.FeaturedAttractions
                .Select(MapAttractionToDto)
                .ToList()
        };
    }

    public async Task<IEnumerable<DestinationLookupDto>> GetDestinationNamesAsync()
    {
        return await _destinationRepository.GetDestinationNamesAsync();
    }

    public async Task<IEnumerable<DestinationLookupDto>> GetPopularDestinationsAsync()
    {
        var popularDestinations = await _destinationRepository.GetPopularDestinationsAsync();
        return popularDestinations.Select(d => new DestinationLookupDto
        {
            Id = d.Id,
            Name = d.Name,
            RegionName = d.RegionName,
            IslandId = d.IslandId,
            IslandName = d.IslandName,
            Description = d.Description,
            ImageUrl = d.ImageUrl,
            Rating = d.Rating,
            Categories = d.Categories
        });
    }

    private DestinationDto MapToDto(Destination destination)
    {
        return new DestinationDto
        {
            Id = destination.Id,
            Name = destination.Name,
            Description = destination.Description,
            ImageUrl = destination.ImageUrl,

            Rating = destination.Rating,
            ReviewCount = destination.ReviewCount,
            IsPopular = destination.IsPopular,

            RegionId = destination.RegionId,
            RegionName = destination.Region?.Name ?? string.Empty,

            IslandId = destination.Region?.IslandId ?? 0,
            IslandName = destination.Region?.Island?.Name ?? string.Empty,

            Categories = destination.DestinationCategories
                .Select(dc => dc.Category.Name)
                .ToList()
        };
    }

    private RegionDto MapRegionToDto(Region region)
    {
        return new RegionDto
        {
            Id = region.Id,
            Name = region.Name,

            IslandId = region.IslandId,
            IslandName = region.Island?.Name ?? string.Empty
        };
    }

    private AttractionDto MapAttractionToDto(Attraction attraction)
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

            // --- UPDATED FIELDS ---
            AvailabilityNote = attraction.AvailabilityNote,
            OpeningHoursNote = attraction.OpeningHoursNote,
            BookingNote = attraction.BookingNote,
            SourceUrl = attraction.SourceUrl,

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
}