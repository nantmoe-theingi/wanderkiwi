using WanderKiwi.Application.DTOs;
using WanderKiwi.Application.Interfaces;
using WanderKiwi.Application.Services;
using WanderKiwi.Domain.Entities;

namespace WanderKiwi.Application.Tests;

public class TripServiceTests
{
    [Fact]
    public async Task CreateAsync_creates_one_day_for_each_trip_date()
    {
        var repository = new InMemoryTripRepository();
        var service = new TripService(repository);

        var trip = await service.CreateAsync(new CreateTripDto
        {
            Name = "South Island Explorer",
            OwnerId = "local-user",
            StartDate = new DateOnly(2026, 10, 1),
            EndDate = new DateOnly(2026, 10, 3),
            BudgetRange = "Mid-range",
            TripStyle = "Road trip"
        });

        Assert.Equal(3, trip.Days.Count);
        Assert.Equal(new DateOnly(2026, 10, 1), trip.Days[0].Date);
        Assert.Equal(new DateOnly(2026, 10, 3), trip.Days[2].Date);
    }

    [Fact]
    public async Task CreateAsync_rejects_an_end_date_before_the_start_date()
    {
        var service = new TripService(new InMemoryTripRepository());

        await Assert.ThrowsAsync<ArgumentException>(() => service.CreateAsync(new CreateTripDto
        {
            Name = "Invalid trip",
            OwnerId = "local-user",
            StartDate = new DateOnly(2026, 10, 3),
            EndDate = new DateOnly(2026, 10, 1)
        }));
    }

    private sealed class InMemoryTripRepository : ITripRepository
    {
        private readonly List<Trip> _trips = new();
        private int _nextTripId = 1;

        public Task<Trip?> GetByIdAsync(int id) =>
            Task.FromResult(_trips.SingleOrDefault(trip => trip.Id == id));

        public Task<List<Trip>> GetByOwnerIdAsync(string ownerId) =>
            Task.FromResult(_trips.Where(trip => trip.OwnerId == ownerId).ToList());

        public Task<Trip> AddAsync(Trip trip)
        {
            trip.Id = _nextTripId++;
            _trips.Add(trip);
            return Task.FromResult(trip);
        }

        public Task UpdateAsync(Trip trip) => Task.CompletedTask;

        public Task DeleteAsync(Trip trip)
        {
            _trips.Remove(trip);
            return Task.CompletedTask;
        }

        public Task<bool> AttractionExistsAsync(int attractionId) => Task.FromResult(attractionId == 1);
    }
}
