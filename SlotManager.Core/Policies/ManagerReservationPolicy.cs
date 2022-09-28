using SlotManager.Core.Entities;
using SlotManager.Core.ValueObjects;

namespace SlotManager.Core.Policies
{
    public class ManagerReservationPolicy : IReservationPolicy
    {
        public bool CanBeApplied(JobTitle jobTitle)
        {
            return jobTitle == JobTitle.Manager;
        }

        public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
        {
            var totalEmployeeReservations = weeklyParkingSpots.SelectMany(x => x.Reservations)
                                                              .OfType<VehicleReservation>()
                                                              .Count(x => x.EmployeeName == employeeName);

            return totalEmployeeReservations > 4;
        }
    }
}
