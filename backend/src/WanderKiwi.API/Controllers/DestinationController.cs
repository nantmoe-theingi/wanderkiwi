using Microsoft.AspNetCore.Mvc;
using WandarKiwi.Application.Interfaces;
using WanderKiwi.Application.DTOs;

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
}