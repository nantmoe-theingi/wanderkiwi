namespace WanderKiwi.Application.Interfaces;

public interface IRouteService
{
    Task<int> GetDrivingMinutesAsync(double originLatitude, double originLongitude,
        double destinationLatitude, double destinationLongitude);
}
