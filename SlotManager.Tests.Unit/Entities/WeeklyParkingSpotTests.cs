using Shouldly;
using SlotManager.Application.Commands;
using SlotManager.Core.Entities;
using SlotManager.Core.Exceptions;
using SlotManager.Core.ValueObjects;
using System;
using Xunit;

namespace SlotManager.Tests.Unit.Entities
{
    public class WeeklyParkingSpotTests
    {
        [Theory]
        [InlineData("2022-08-09")]
        [InlineData("2022-09-28")]
        public void given_invalid_date_add_reservation_should_fail(string dateString)
        {
            var invalidDate = DateTime.Parse(dateString);
            var reservation = new VehicleReservation(Guid.NewGuid(), _weeklyParkingSpot.Id, new Date(invalidDate), "John Smith", "XYZ123");

            var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, _now));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidReservationDateException>();
        }

        [Fact]
        public void given_reservation_for_already_existing_date_add_reservation_should_fail()
        {
            var reservationDate = _now.AddDays(1);
            var reservation = new VehicleReservation(Guid.NewGuid(), _weeklyParkingSpot.Id, reservationDate, "John Black", "XYZ456");
            var nextReservation = new VehicleReservation(Guid.NewGuid(), _weeklyParkingSpot.Id, reservationDate, "John Black", "XYZ456");
            _weeklyParkingSpot.AddReservation(reservation, _now);

            var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(nextReservation, reservationDate));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ParkingSpotAlreadyReservedException>();
        }

        [Fact]
        public void given_reservation_for_nottaken_date_add_reservation_should_succeed()
        {
            var reservationDate = _now.AddDays(1);
            var reservation = new VehicleReservation(Guid.NewGuid(), _weeklyParkingSpot.Id, reservationDate, "John Black", "XYZ456");

            _weeklyParkingSpot.AddReservation(reservation, _now);

            _weeklyParkingSpot.Reservations.ShouldHaveSingleItem();
        }

        #region Arrange

        private readonly WeeklyParkingSpot _weeklyParkingSpot;
        private readonly Date _now;

        public WeeklyParkingSpotTests()
        {
            _now = new Date(new DateTime(2022, 08, 10));
            _weeklyParkingSpot = new WeeklyParkingSpot(Guid.NewGuid(), new Week(_now), "P1");
        }

        #endregion Arrange
    }
}
