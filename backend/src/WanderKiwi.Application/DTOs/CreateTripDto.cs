using System.ComponentModel.DataAnnotations;

namespace WanderKiwi.Application.DTOs;

public class CreateTripDto
{
    [Required, StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Required, StringLength(64)]
    public string OwnerId { get; set; } = string.Empty;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    [StringLength(50)]
    public string BudgetRange { get; set; } = string.Empty;

    [StringLength(50)]
    public string TripStyle { get; set; } = string.Empty;

    public List<string> Interests { get; set; } = new();
}
