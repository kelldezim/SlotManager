using SlotManager.Core.ValueObjects;

namespace SlotManager.Core.Entities
{
    public abstract class Reservation
    {
        protected Reservation() 
        {
        }

        public Reservation(ReservationId id,
                           ParkingSpotId parkingSpotId,
                           Date date)
        {
            Id = id;
            ParkingSpotId = parkingSpotId;
            Date = date;
        }

        public ReservationId Id { get; private set; }
        public ParkingSpotId ParkingSpotId { get; private set; }
        public Date Date { get; private set; }

    }
}
