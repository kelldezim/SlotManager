using SlotManager.Core.Abstractions;
using SlotManager.Core.Entities;
using SlotManager.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotManager.Core.Policies
{
    internal sealed class RegularEmployeeReservationPolicy : IReservationPolicy
    {
        private readonly IClock _clock;

        public RegularEmployeeReservationPolicy(IClock clock)
        {
            _clock = clock;
        }

        public bool CanBeApplied(JobTitle jobTitle)
        {
            return jobTitle == JobTitle.Employee;
        }

        public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
        {
            var totalEmployeeReservations = weeklyParkingSpots.SelectMany(x => x.Reservations)
                                                              .OfType<VehicleReservation>()
                                                              .Count(x => x.EmployeeName == employeeName);

            return totalEmployeeReservations < 2 && _clock.Current().Hour > 4;
        }
    }
}
