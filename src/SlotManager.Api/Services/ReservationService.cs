using SlotManager.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotManager.Api.Services
{
    public class ReservationService
    {
        private static int _id = 1;
        private static readonly List<Reservation> _reservations = new();
        private static readonly List<string> _parkingSlots = new()
        {
            "P1",
            "P2",
            "P3",
            "P4",
            "P5"
        };

        public Reservation Get(int id)
        {
            return _reservations.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _reservations;
        }

        public int? Create(Reservation reservation)
        {
            if (_parkingSlots.All(x => x != reservation.ParkingSpotName))
            {
                return default;
            }

            reservation.Date = DateTime.UtcNow.AddDays(1).Date;

            var reservationAlreadyExists = _reservations.Any(x => x.ParkingSpotName == reservation.ParkingSpotName
                                                                  && x.Date.Date == reservation.Date.Date);

            if (reservationAlreadyExists)
            {
                return default;
            }

            reservation.Id = _id;

            _id++;
            _reservations.Add(reservation);

            return reservation.Id;
        }

        public bool Update(Reservation reservation)
        {
            var existingReservation = _reservations.SingleOrDefault(x => x.Id == reservation.Id);

            if (existingReservation is null)
            {
                return false;
            }

            existingReservation.LicensePlate = reservation.LicensePlate;

            return true;
        }

        public bool Delete(int id)
        {
            var existingReservation = _reservations.SingleOrDefault(x => x.Id == id);

            if (existingReservation is null)
            {
                return false;
            }

            _reservations.Remove(existingReservation);

            return true;
        }
    }
}
