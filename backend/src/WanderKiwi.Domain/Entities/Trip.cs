namespace WanderKiwi.Domain.Entities;

public class Trip
{
    public int Id { get; set; }

    // Temporary owner key until ASP.NET Identity is introduced.
    public string OwnerId { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public string BudgetRange { get; set; } = string.Empty;

    public string TripStyle { get; set; } = string.Empty;

    public ICollection<TripDay> Days { get; set; } = new List<TripDay>();
}
