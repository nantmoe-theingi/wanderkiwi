using WanderKiwi.Application.Models;
using WanderKiwi.Domain.DTOs;
using WanderKiwi.Domain.Entities;

namespace WanderKiwi.Application.Interfaces;

public interface IDestinationRepository
{

    Task<DestinationPageData> GetDestinationPageAsync();

    Task<IEnumerable<DestinationLookupDto>> GetDestinationNamesAsync();

    Task<IEnumerable<DestinationLookup>> GetPopularDestinationsAsync();

    Task<Destination> GetByIdAsync(int id);

    Task<Destination> GetbyNameAsync(string name);
}