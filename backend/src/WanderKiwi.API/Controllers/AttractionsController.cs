using Microsoft.AspNetCore.Mvc;
using WanderKiwi.Application.DTOs;
using WanderKiwi.Application.Interfaces;

namespace WanderKiwi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttractionsController : ControllerBase
{
    private readonly IAttractionService _attractionService;

    // Inject the Application Service
    public AttractionsController(IAttractionService attractionService)
    {
        _attractionService = attractionService;
    }

    // GET: api/attractions?search=mountain&region=canterbury
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AttractionDto>>> GetAttractions([FromQuery] string? search, [FromQuery] string? region)
    {
        var attractions = await _attractionService.SearchAttractionsAsync(search, region);

        // Returns HTTP 200 OK with the list of DTOs
        return Ok(attractions);
    }

    // GET: api/attractions/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AttractionDto>> GetAttraction(int id)
    {
        var attraction = await _attractionService.GetAttractionByIdAsync(id);

        if (attraction == null)
        {
            // Returns HTTP 404 Not Found if the attraction doesn't exist
            return NotFound();
        }

        // Returns HTTP 200 OK with the specific attraction details
        return Ok(attraction);
    }

    // POST: api/attractions
    [HttpPost]
    public async Task<ActionResult<AttractionDto>> CreateAttraction(AttractionDto attractionDto)
    {
        var createdAttraction = await _attractionService.CreateAttractionAsync(attractionDto);
        return CreatedAtAction(nameof(GetAttraction), new { id = createdAttraction.Id }, createdAttraction);
    }

    // PUT: api/attractions/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAttraction(int id, AttractionDto attractionDto)
    {
        if (id != attractionDto.Id)
        {
            return BadRequest();
        }

        var result = await _attractionService.UpdateAttractionAsync(id, attractionDto);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    // DELETE: api/attractions/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAttraction(int id)
    {
        var result = await _attractionService.DeleteAttractionAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}