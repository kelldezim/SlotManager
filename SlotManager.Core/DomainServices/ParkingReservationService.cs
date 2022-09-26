using SlotManager.Core.Entities;
using SlotManager.Core.ValueObjects;

namespace SlotManager.Core.DomainServices
{
    public sealed class ParkingReservationService : IParkingReservationService
    {
        public void ReserveSpotForVehicle(IEnumerable<WeeklyParkingSpot> allParkingSpots, 
                                          JobTitle jobTitle, 
                                          WeeklyParkingSpot weeklyParkingSpotToReserve, 
                                          Reservation reservation)
        {
            throw new NotImplementedException();
        }
    }
}
