using SlotManager.Core.Entities;
using SlotManager.Core.Repositories;
using SlotManager.Core.ValueObjects;
using SlotManager.Infrastructure.Time;

namespace SlotManager.Infrastructure.DAL.Repositories
{
    internal class InMemoryWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
    {
        private readonly IClock _clock;
        private readonly List<WeeklyParkingSpot> _weeklyParkingSpots;

        public InMemoryWeeklyParkingSpotRepository(IClock clock)
        {
            _clock = clock;
            _weeklyParkingSpots = new List<WeeklyParkingSpot>
            {
                new (Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(_clock.Current()), "P1"),
                new (Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(_clock.Current()), "P2"),
                new (Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(_clock.Current()), "P3"),
                new (Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(_clock.Current()), "P4"),
                new (Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(_clock.Current()), "P5")
            };
        }

        public Task AddAsync(WeeklyParkingSpot weeklyParkingSpot)
        {
            _weeklyParkingSpots.Add(weeklyParkingSpot);

            return Task.CompletedTask;
        }

        public Task DeleteAsync(WeeklyParkingSpot weeklyParkingSpot)
        {
            _weeklyParkingSpots.Remove(weeklyParkingSpot);

            return Task.CompletedTask;
        }

        public Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id)
        {
            return Task.FromResult(_weeklyParkingSpots.SingleOrDefault(x => x.Id == id));
        }

        public Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync()
        {
            return Task.FromResult(_weeklyParkingSpots.AsEnumerable());
        }

        public Task UpdateAsync(WeeklyParkingSpot weeklyParkingSpot)
        {
            return Task.CompletedTask;
        }
    }
}
