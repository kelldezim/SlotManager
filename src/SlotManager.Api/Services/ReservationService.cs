using SlotManager.Api.Commands;
using SlotManager.Api.DTO;
using SlotManager.Api.Entities;
using SlotManager.Api.ValueObjects;

namespace SlotManager.Api.Services
{
    public class ReservationService
    {
        private static Clock Clock = new Clock();
        private static readonly List<WeeklyParkingSpot> WeeklyParkingSpots = new()
        {
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(Clock.Current()), "P1"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(Clock.Current()), "P2"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(Clock.Current()), "P3"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(Clock.Current()), "P4"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(Clock.Current()), "P5")
        };

        public ReservationDto Get(Guid id)
        {
            return GetAllWeekly().SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<ReservationDto> GetAllWeekly()
        {
            return WeeklyParkingSpots.SelectMany(x => x.Reservations)
                .Select(x => new ReservationDto
                {
                    Id = x.Id,
                    ParkingSpotId = x.ParkingSpotId,
                    EmployeeName = x.EmployeeName,
                    Date = x.Date.Value.Date
                });
        }

        public Guid? Create(CreateReservation command)
        {
            var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
            var weeklyParkingSpot = WeeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);

            if(weeklyParkingSpot == null)
            {
                return default;
            }

            var reservation = new Reservation(command.ReservationId,
                                              command.ParkingSpotId,
                                              command.EmployeeName,
                                              command.LicensePlate,
                                              new Date(command.Date));

            weeklyParkingSpot.AddReservation(reservation, new Date(Clock.Current()));

            return reservation.Id;
        }

        public bool Update(ChangeReservationLicensePlate command)
        {
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);

            if(weeklyParkingSpot is null)
            {
                return false;
            }

            var reservationId = new ReservationId(command.ReservationId);
            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == reservationId);

            if (existingReservation is null)
            {
                return false;
            }

            if (existingReservation.Date <= new Date(Clock.Current()))
            {
                return false;
            }

            existingReservation.ChangeLicencePlate(command.LicensePlate);

            return true;
        }

        public bool Delete(DeleteReservation command)
        {
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);

            if(weeklyParkingSpot is null)
            {
                return false;
            }

            var existingReservationId = new ReservationId(command.ReservationId);
            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == existingReservationId);

            if (existingReservation is null)
            {
                return false;
            }

            weeklyParkingSpot.RemoveReservation(command.ReservationId);

            return true;
        }

        private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(ReservationId reservationId)
        {
            return WeeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(r => r.Id == reservationId));
        }
    }
}
