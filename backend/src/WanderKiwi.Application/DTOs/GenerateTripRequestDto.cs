namespace WanderKiwi.Application.DTOs;

public class GenerateTripRequestDto
{
    public int DestinationId { get; set; } =0;
    public string Destination { get; set; } = string.Empty;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int NumberOfDays { get; set; }

    public TimeSpan StartTime { get; set; }

    public int Travellers { get; set; }

    public string TripStyle { get; set; } = string.Empty;

    public List<string> Interests { get; set; } = [];

    public string Budget { get; set; } = string.Empty;

    public string TransportMode { get; set; } = "Car";
}