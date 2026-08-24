using System.ComponentModel.DataAnnotations;

namespace WanderKiwi.Application.DTOs;

public class AddTripStopDto
{
    public int? AttractionId { get; set; }

    [StringLength(120)]
    public string? CustomName { get; set; }

    [Range(0, 1440)]
    public int? PlannedDurationMinutes { get; set; }

    [StringLength(1000)]
    public string? Notes { get; set; }
}
