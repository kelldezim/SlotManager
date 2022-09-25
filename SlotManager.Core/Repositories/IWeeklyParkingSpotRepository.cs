using SlotManager.Core.Entities;
using SlotManager.Core.ValueObjects;

namespace SlotManager.Core.Repositories
{
    public interface IWeeklyParkingSpotRepository
    {
        Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync();
        Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id);
        Task AddAsync(WeeklyParkingSpot weeklyParkingSpot);
        Task UpdateAsync(WeeklyParkingSpot weeklyParkingSpot);
        Task DeleteAsync(WeeklyParkingSpot weeklyParkingSpot);
    }
}
