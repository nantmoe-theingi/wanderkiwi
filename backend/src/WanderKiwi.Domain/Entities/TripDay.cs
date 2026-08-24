namespace WanderKiwi.Domain.Entities;

public class TripDay
{
    public int Id { get; set; }

    public int TripId { get; set; }

    public Trip Trip { get; set; } = null!;

    public int DayNumber { get; set; }

    public DateOnly Date { get; set; }

    public ICollection<TripStop> Stops { get; set; } = new List<TripStop>();
}
