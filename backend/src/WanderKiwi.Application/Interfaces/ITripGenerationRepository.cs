using WanderKiwi.Application.DTOs;
using WanderKiwi.Domain.Entities;

namespace WanderKiwi.Application.Interfaces;

public interface ITripGenerationRepository
{
    Task<GeneratedTripItineraryDto> GenerateItineraryAsync(GenerateTripRequestDto request);
    //Task<List<Attraction>> GetByDestinationAsync(string destinationName);
}