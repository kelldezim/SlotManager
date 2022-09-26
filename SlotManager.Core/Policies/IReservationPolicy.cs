using SlotManager.Core.Entities;
using SlotManager.Core.ValueObjects;

namespace SlotManager.Core.Policies
{
    public interface IReservationPolicy
    {
        bool CanBeApplied(JobTitle jobTitle);
        bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName);
    }
}
