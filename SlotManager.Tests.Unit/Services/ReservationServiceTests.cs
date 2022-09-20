using SlotManager.Api.Commands;
using SlotManager.Api.Services;
using Xunit;
using System;
using Shouldly;
using SlotManager.Api.Entities;
using System.Collections.Generic;
using SlotManager.Api.ValueObjects;
using System.Linq;

namespace SlotManager.Tests.Unit.Services
{
    public class ReservationServiceTests
    {
        [Fact]
        public void given_reservation_for_nottaken_date_add_reservation_should_succeed()
        {
            var weeklyParkingSpot = _weeklyParkingSpots.First();
            var command = new CreateReservation(weeklyParkingSpot.Id,
                                                Guid.NewGuid(),
                                                DateTime.UtcNow.AddMinutes(5),
                                                "John Deep",
                                                "XYZ123");

            var reservationId = _reservationService.Create(command);

            reservationId.ShouldNotBeNull();
            reservationId.Value.ShouldBe(command.ReservationId);
        }

        #region Arrange

        private static readonly Clock Clock = new Clock();
        private readonly ReservationService _reservationService;
        private static readonly List<WeeklyParkingSpot> _weeklyParkingSpots = new()
        {
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(Clock.Current()), "P1"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(Clock.Current()), "P2"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(Clock.Current()), "P3"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(Clock.Current()), "P4"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(Clock.Current()), "P5")
        };

        public ReservationServiceTests()
        {
            _reservationService = new ReservationService(_weeklyParkingSpots);
        }

        #endregion
    }
}
