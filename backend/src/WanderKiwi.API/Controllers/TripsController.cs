using Microsoft.AspNetCore.Mvc;
using WanderKiwi.Application.DTOs;
using WanderKiwi.Application.Interfaces;

namespace WanderKiwi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripsController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TripDto>>> GetByOwner([FromQuery] string ownerId)
    {
        if (string.IsNullOrWhiteSpace(ownerId))
        {
            return BadRequest("An ownerId is required.");
        }

        return Ok(await _tripService.GetByOwnerIdAsync(ownerId));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TripDto>> GetById(int id)
    {
        var trip = await _tripService.GetByIdAsync(id);
        return trip is null ? NotFound() : Ok(trip);
    }

    [HttpPost]
    public async Task<ActionResult<TripDto>> Create(CreateTripDto request)
    {
        try
        {
            var trip = await _tripService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = trip.Id }, trip);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, UpdateTripDto request)
    {
        try
        {
            return await _tripService.UpdateAsync(id, request) ? NoContent() : NotFound();
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
        catch (InvalidOperationException exception)
        {
            return Conflict(exception.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await _tripService.DeleteAsync(id) ? NoContent() : NotFound();
    }

    [HttpPost("{tripId:int}/days/{dayNumber:int}/stops")]
    public async Task<ActionResult<TripStopDto>> AddStop(int tripId, int dayNumber, AddTripStopDto request)
    {
        try
        {
            var stop = await _tripService.AddStopAsync(tripId, dayNumber, request);
            return stop is null ? NotFound("Trip or day was not found.") : Ok(stop);
        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
