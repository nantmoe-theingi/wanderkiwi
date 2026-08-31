using WanderKiwi.Application.DTOs;
using WanderKiwi.Domain.DTOs;

namespace WandarKiwi.Application.Interfaces;

public interface IDestinationService
{
    Task<DestinationPageDto> GetDestinationPageAsync();

    Task<IEnumerable<DestinationLookupDto>> GetDestinationNamesAsync();

    Task<IEnumerable<DestinationLookupDto>> GetPopularDestinationsAsync();
}