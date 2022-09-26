using SlotManager.Application.Commands;
using SlotManager.Application.DTO;
using SlotManager.Core.Abstractions;
using SlotManager.Core.DomainServices;
using SlotManager.Core.Entities;
using SlotManager.Core.Repositories;
using SlotManager.Core.ValueObjects;

namespace SlotManager.Application.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IClock _clock;
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
        private readonly IParkingReservationService _parkingReservationService;

        public ReservationService(
            IClock clock,
            IWeeklyParkingSpotRepository weeklyParkingSpotRepository,
            IParkingReservationService parkingReservationService)
        {
            _clock = clock;
            _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
            _parkingReservationService = parkingReservationService;
        }

        public async Task<ReservationDto> GetAsync(Guid id)
        {
            var reservations = await GetAllWeeklyAsync();
            return reservations.SingleOrDefault(x => x.Id == id);
        }

        public async Task<IEnumerable<ReservationDto>> GetAllWeeklyAsync()
        {
            var weeklyParkingSpots = await _weeklyParkingSpotRepository.GetAllAsync();

            return weeklyParkingSpots.SelectMany(x => x.Reservations)
                .Select(x => new ReservationDto
                {
                    Id = x.Id,
                    ParkingSpotId = x.ParkingSpotId,
                    EmployeeName = x.EmployeeName,
                    Date = x.Date.Value.Date
                });
        }

        public async Task<Guid?> CreateAsync(CreateReservation command)
        {
            var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
            var week = new Week(_clock.Current());

            var weeklyParkingSpots = (await _weeklyParkingSpotRepository.GetByWeekAsync(week)).ToList();
            var parkingSpotToReserve = weeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);

            if (parkingSpotToReserve == null)
            {
                return default;
            }

            var reservation = new Reservation(command.ReservationId,
                                              command.ParkingSpotId,
                                              command.EmployeeName,
                                              command.LicensePlate,
                                              new Date(command.Date));

            _parkingReservationService.ReserveSpotForVehicle(weeklyParkingSpots, JobTitle.Employee, parkingSpotToReserve, reservation);

            await _weeklyParkingSpotRepository.UpdateAsync(parkingSpotToReserve);

            return reservation.Id;
        }

        public async Task<bool> UpdateAsync(ChangeReservationLicensePlate command)
        {
            var weeklyParkingSpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);

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
            await _weeklyParkingSpotRepository.UpdateAsync(weeklyParkingSpot);

            return true;
        }

        public async Task<bool> DeleteAsync(DeleteReservation command)
        {
            var weeklyParkingSpot = await GetWeeklyParkingSpotByReservationAsync(command.ReservationId);

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
            await _weeklyParkingSpotRepository.DeleteAsync(weeklyParkingSpot);

            return true;
        }

        private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservationAsync(ReservationId reservationId)
        {
            var weeklyParkingSpots = await _weeklyParkingSpotRepository.GetAllAsync();

            return weeklyParkingSpots.SingleOrDefault(x => x.Reservations.Any(r => r.Id == reservationId));
        }
    }
}
