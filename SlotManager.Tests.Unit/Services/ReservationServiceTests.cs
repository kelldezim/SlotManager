using Xunit;
using System;
using Shouldly;
using System.Linq;
using SlotManager.Tests.Unit.Shared;
using SlotManager.Core.Repositories;
using SlotManager.Application.Commands;
using SlotManager.Application.Services;
using SlotManager.Infrastructure.Time;
using SlotManager.Infrastructure.Repositories;

namespace SlotManager.Tests.Unit.Services
{
    public class ReservationServiceTests
    {
        [Fact]
        public void given_reservation_for_nottaken_date_add_reservation_should_succeed()
        {
            var weeklyParkingSpot = _weeklyParkingSpotRepository.GetAll().First();
            var command = new CreateReservation(weeklyParkingSpot.Id,
                                                Guid.NewGuid(),
                                                _clock.Current(),
                                                "John Deep",
                                                "XYZ123");

            var reservationId = _reservationService.Create(command);

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
