using SlotManager.Core.Entities;
using SlotManager.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotManager.Core.DomainServices
{
    public interface IParkingReservationService
    {
        void ReserveSpotForVehicle(IEnumerable<WeeklyParkingSpot> allParkingSpots,
                                    JobTitle jobTitle,
                                    WeeklyParkingSpot weeklyParkingSpotToReserve,
                                    Reservation reservation);
    }
}
