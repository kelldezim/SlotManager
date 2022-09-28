using SlotManager.Core.ValueObjects;

namespace SlotManager.Core.Entities
{
    public class CleaningReservation : Reservation
    {
        private CleaningReservation()
        {
        }

        public CleaningReservation(
            ReservationId id,
            ParkingSpotId parkingSpotId,
            Date date)
            : base(id, parkingSpotId, date)
        {
        }
    }
}
