using SlotManager.Application.Commands;
using SlotManager.Application.DTO;
using SlotManager.Core.Entities;
using SlotManager.Core.Repositories;
using SlotManager.Core.ValueObjects;
using SlotManager.Infrastructure.Time;

namespace SlotManager.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IClock _clock;
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;

        public ReservationService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository)
        {
            _clock = clock;
            _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
        }

        public ReservationDto Get(Guid id)
        {
            return GetAllWeekly().SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<ReservationDto> GetAllWeekly()
        {
            return _weeklyParkingSpotRepository.GetAll().SelectMany(x => x.Reservations)
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
            var weeklyParkingSpot = _weeklyParkingSpotRepository.Get(parkingSpotId);

            if (weeklyParkingSpot == null)
            {
                return default;
            }

            var reservation = new Reservation(command.ReservationId,
                                              command.ParkingSpotId,
                                              command.EmployeeName,
                                              command.LicensePlate,
                                              new Date(command.Date));

            weeklyParkingSpot.AddReservation(reservation, new Date(_clock.Current()));
            _weeklyParkingSpotRepository.Update(weeklyParkingSpot);

            return reservation.Id;
        }

        public bool Update(ChangeReservationLicensePlate command)
        {
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);

            if (weeklyParkingSpot is null)
            {
                return false;
            }

            var reservationId = new ReservationId(command.ReservationId);
            var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == reservationId);

            if (existingReservation is null)
            {
                return false;
            }

            if (existingReservation.Date <= new Date(_clock.Current()))
            {
                return false;
            }

            existingReservation.ChangeLicencePlate(command.LicensePlate);
            _weeklyParkingSpotRepository.Update(weeklyParkingSpot);

            return true;
        }

        public bool Delete(DeleteReservation command)
        {
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);

            if (weeklyParkingSpot is null)
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
            _weeklyParkingSpotRepository.Delete(weeklyParkingSpot);

            return true;
        }

        private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(ReservationId reservationId)
        {
            return _weeklyParkingSpotRepository.GetAll().SingleOrDefault(x => x.Reservations.Any(r => r.Id == reservationId));
        }
    }
}
