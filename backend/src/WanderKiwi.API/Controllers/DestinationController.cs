using Microsoft.AspNetCore.Mvc;
using WandarKiwi.Application.Interfaces;
using WanderKiwi.Application.DTOs;
using WanderKiwi.Domain.DTOs;

namespace WanderKiwi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DestinationsController : ControllerBase
{
    private readonly IDestinationService _destinationService;

    public DestinationsController(IDestinationService destinationService)
    {
        _destinationService = destinationService;
    }

    [HttpGet("page")]
    public async Task<ActionResult<DestinationPageDto>>
    GetDestinationPage()
    {
        var page =
            await _destinationService.GetDestinationPageAsync();

        return Ok(page);
    }

    // GET: api/Destinations/names
    [HttpGet("names")]
    public async Task<ActionResult<IEnumerable<DestinationLookupDto>>> GetDestinationNames()
    {
        var destinations = await _destinationService.GetDestinationNamesAsync();

        return Ok(destinations);
    }

    // GET: api/Destinations/popular
    [HttpGet("popular")]
    public async Task<ActionResult<IEnumerable<DestinationLookupDto>>> GetPopularDestinations()
    {
        var popularDestinations = await _destinationService.GetPopularDestinationsAsync();
        return Ok(popularDestinations);

    }
}