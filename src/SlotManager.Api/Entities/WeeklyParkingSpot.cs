using SlotManager.Api.Exceptions;
using SlotManager.Api.ValueObjects;

namespace SlotManager.Api.Entities
{
    public class WeeklyParkingSpot
    {
        private readonly HashSet<Reservation> _reservations = new();

        public WeeklyParkingSpot(ParkingSpotId id, Week week, ParkingSpotName name)
        {
            Id = id;
            Week = week;
            Name = name;
        }

        public ParkingSpotId Id { get; }
        public Week Week { get; set; }
        public ParkingSpotName Name { get; }
        public IEnumerable<Reservation> Reservations => _reservations;

        public void AddReservation(Reservation reservation, Date now)
        {
            var isInvalidDate = reservation.Date < Week.From || 
                                reservation.Date > Week.To || 
                                reservation.Date < now;

            if(isInvalidDate)
            {
                throw new InvalidReservationDateException(reservation.Date.Value.Date);
            }

            var reservationAlreadyExist = _reservations.Any(x => x.Date == reservation.Date);

            if (reservationAlreadyExist)
            {
                throw new ParkingSpotAlreadyReservedException(reservation.Date.Value.Date, this.Name);
            }

            _reservations.Add(reservation);
        }

        public void RemoveReservation(ReservationId id)
        {
            var reservation = _reservations.SingleOrDefault(x => x.Id == id);

            _reservations.Remove(reservation);
        }
    }
}
