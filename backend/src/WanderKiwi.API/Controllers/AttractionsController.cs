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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AttractionDto>>> GetAll()
    {
        var attractions = await _attractionService.GetAllAsync();

        return Ok(attractions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AttractionDto>> GetById(int id)
    {
        var attraction = await _attractionService.GetByIdAsync(id);

        if (attraction == null)
        {
            return NotFound();
        }

        return Ok(attraction);
    }

    // POST: api/attractions
    [HttpPost]
    public async Task<ActionResult<AttractionDto>> CreateAttraction(AttractionDto attractionDto)
    {
        var createdAttraction = await _attractionService.CreateAsync(attractionDto);
        return CreatedAtAction(nameof(GetById), new { id = createdAttraction.Id }, createdAttraction);
    }

    // PUT: api/attractions/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAttraction(int id, AttractionDto attractionDto)
    {
        if (id != attractionDto.Id)
        {
            return BadRequest();
        }

        var result = await _attractionService.UpdateAsync(id, attractionDto);

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
        var result = await _attractionService.DeleteAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<AttractionDto>>> Search(
        [FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("Search query cannot be empty.");
        }

        var attractions = await _attractionService.SearchAsync(query);

        return Ok(attractions);
    }
}