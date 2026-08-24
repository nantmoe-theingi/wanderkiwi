namespace WanderKiwi.Application.DTOs;

public class GenerateTripRequestDto
{
    public int DestinationId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Travelers { get; set; } = string.Empty;
    public string TripStyle { get; set; } = string.Empty;
    public List<string> Interests { get; set; } = new();
    public string BudgetRange { get; set; } = string.Empty;
}