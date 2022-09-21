using SlotManager.Core.Entities;
using SlotManager.Core.ValueObjects;

namespace SlotManager.Core.Repositories
{
    public interface IWeeklyParkingSpotRepository
    {
        public IEnumerable<WeeklyParkingSpot> GetAll();
        public WeeklyParkingSpot Get(ParkingSpotId id);
        void Add(WeeklyParkingSpot weeklyParkingSpot);
        void Update(WeeklyParkingSpot weeklyParkingSpot);
        void Delete(WeeklyParkingSpot weeklyParkingSpot);
    }
}
