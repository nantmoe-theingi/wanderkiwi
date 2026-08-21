using WanderKiwi.Application.DTOs;

namespace WandarKiwi.Application.Interfaces;

public interface IDestinationService
{
    Task<DestinationPageDto> GetDestinationPageAsync();
}