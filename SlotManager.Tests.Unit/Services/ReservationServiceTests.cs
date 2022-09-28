using Xunit;
using System;
using Shouldly;
using System.Linq;
using SlotManager.Tests.Unit.Shared;
using SlotManager.Core.Repositories;
using SlotManager.Application.Commands;
using SlotManager.Application.Services;
using SlotManager.Infrastructure.DAL.Repositories;
using System.Threading.Tasks;
using SlotManager.Core.Abstractions;
using SlotManager.Core.DomainServices;
using SlotManager.Core.Policies;

namespace SlotManager.Tests.Unit.Services
{
    public class ReservationServiceTests
    {
        //Require refactor according to new policies

        //[Fact]
        public async Task given_reservation_for_nottaken_date_add_reservation_should_succeed()
        {
            var weeklyParkingSpot = (await _weeklyParkingSpotRepository.GetAllAsync()).First();
            var command = new ReserveParkingSpotForVehicle(weeklyParkingSpot.Id,
                                                Guid.NewGuid(),
                                                _clock.Current(),
                                                "John Deep",
                                                "XYZ123");

            var reservationId = await _reservationService.ReserveForVehicleAsync(command);

            reservationId.ShouldNotBeNull();
            reservationId.Value.ShouldBe(command.ReservationId);
        }

        #region Arrange

        private readonly IClock _clock;
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
        private readonly IReservationService _reservationService;

        public ReservationServiceTests()
        {
            _clock = new TestClock();
            _weeklyParkingSpotRepository = new InMemoryWeeklyParkingSpotRepository(_clock);
            
            var parkingReservationService = new ParkingReservationService(new IReservationPolicy[]
            {
                new BossReservationPolicy(),
                new ManagerReservationPolicy(),
                new RegularEmployeeReservationPolicy(_clock)
            }, _clock);

            _reservationService = new ReservationService(_clock, _weeklyParkingSpotRepository, parkingReservationService);
        }

        #endregion
    }
}
