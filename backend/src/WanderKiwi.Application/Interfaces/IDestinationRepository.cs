using WanderKiwi.Application.DTOs;
using WanderKiwi.Application.Models;
using WanderKiwi.Domain.Entities;

namespace WanderKiwi.Application.Interfaces;

public interface IDestinationRepository
{

    Task<DestinationPageData> GetDestinationPageAsync();

    Task<Destination> GetByIdAsync(int id);
}