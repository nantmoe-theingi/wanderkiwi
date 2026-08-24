using WanderKiwi.Application.DTOs;

namespace WanderKiwi.Application.Interfaces;

public interface ITripGenerationService
{
    Task<GeneratedTripItineraryDto> GenerateItineraryAsync(GenerateTripRequestDto request);
}