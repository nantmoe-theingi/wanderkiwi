using Microsoft.AspNetCore.Mvc;
using WanderKiwi.Application.DTOs;
using WanderKiwi.Application.Interfaces;

namespace WanderKiwi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;
    private readonly ITripGenerationService _tripGenerationService;

    public TripsController(
        ITripService tripService, 
        ITripGenerationService tripGenerationService)
    {
        _tripService = tripService;
        _tripGenerationService = tripGenerationService;
    }

    /// <summary>
    /// Generates a customized day-by-day travel itinerary based on user preferences.
    /// </summary>
    /// <param name="request">Trip criteria including destination, dates, trip style, and interests.</param>
    /// <returns>A structured itinerary with daily scheduled stops.</returns>
    [HttpPost("generate")]
    [ProducesResponseType(typeof(GeneratedTripItineraryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GenerateTrip([FromBody] GenerateTripRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var result = await _tripGenerationService.GenerateItineraryAsync(request);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return NotFound(new { message = ex.Message });
        }
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
