using SlotManager.Core.Entities;
using SlotManager.Core.ValueObjects;

namespace SlotManager.Core.Policies
{
    internal sealed class BossReservationPolicy : IReservationPolicy
    {
        public bool CanBeApplied(JobTitle jobTitle)
        {
            return jobTitle == JobTitle.Boss;
        }

        public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
        {
            return true;
        }
    }
}
