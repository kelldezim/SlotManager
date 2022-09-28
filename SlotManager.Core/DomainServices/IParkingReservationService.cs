using SlotManager.Core.Entities;
using SlotManager.Core.ValueObjects;

namespace SlotManager.Core.DomainServices
{
    public interface IParkingReservationService
    {
        void ReserveSpotForVehicle(IEnumerable<WeeklyParkingSpot> allParkingSpots,
                                    JobTitle jobTitle,
                                    WeeklyParkingSpot weeklyParkingSpotToReserve,
                                    VehicleReservation reservation);
        void ReserveParkingForCleaning(IEnumerable<WeeklyParkingSpot> allParkingSpots,
                                    Date date);
    }
}
