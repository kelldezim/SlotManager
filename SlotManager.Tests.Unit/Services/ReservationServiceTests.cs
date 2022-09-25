using Xunit;
using System;
using Shouldly;
using System.Linq;
using SlotManager.Tests.Unit.Shared;
using SlotManager.Core.Repositories;
using SlotManager.Application.Commands;
using SlotManager.Application.Services;
using SlotManager.Infrastructure.Time;
using SlotManager.Infrastructure.DAL.Repositories;
using System.Threading.Tasks;

namespace SlotManager.Tests.Unit.Services
{
    public class ReservationServiceTests
    {
        [Fact]
        public async Task given_reservation_for_nottaken_date_add_reservation_should_succeed()
        {
            var weeklyParkingSpot = (await _weeklyParkingSpotRepository.GetAllAsync()).First();
            var command = new CreateReservation(weeklyParkingSpot.Id,
                                                Guid.NewGuid(),
                                                _clock.Current(),
                                                "John Deep",
                                                "XYZ123");

            var reservationId = await _reservationService.CreateAsync(command);

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
            _reservationService = new ReservationService(_clock, _weeklyParkingSpotRepository);
        }

        #endregion
    }
}
