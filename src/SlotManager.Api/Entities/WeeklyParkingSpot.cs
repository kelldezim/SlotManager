using SlotManager.Api.Exceptions;

namespace SlotManager.Api.Entities
{
    public class WeeklyParkingSpot
    {
        private readonly HashSet<Reservation> _reservations = new();

        public WeeklyParkingSpot(Guid id, DateTime from, DateTime to, string name)
        {
            Id = id;
            From = from;
            To = to;
            Name = name;
        }

        public Guid Id { get; }
        public DateTime From { get; }
        public DateTime To { get; }
        public string Name { get; }
        public IEnumerable<Reservation> Reservations => _reservations;

        public void AddReservation(Reservation reservation)
        {
            var isInvalidDate = reservation.Date.Date < From || 
                                reservation.Date.Date > To || 
                                reservation.Date.Date < DateTime.UtcNow.Date;

            if(isInvalidDate)
            {
                throw new InvalidReservationDateException(reservation.Date);
            }

            var reservationAlreadyExist = _reservations.Any(x => x.Date.Date == reservation.Date.Date);

            if (reservationAlreadyExist)
            {
                throw new ParkingSpotAlreadyReservedException(reservation.Date, this.Name);
            }

            _reservations.Add(reservation);
        }

        public void RemoveReservation(Guid id)
        {
            var reservation = _reservations.SingleOrDefault(x => x.Id == id);

            _reservations.Remove(reservation);
        }
    }
}
