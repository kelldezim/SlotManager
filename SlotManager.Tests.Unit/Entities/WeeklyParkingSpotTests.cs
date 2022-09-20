using Shouldly;
using SlotManager.Api.Entities;
using SlotManager.Api.Exceptions;
using SlotManager.Api.ValueObjects;
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
            var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Smith", "XYZ123", new Date(invalidDate));

            var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, new Date(_now)));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidReservationDateException>();
        }

        [Fact]
        public void given_reservation_for_already_existing_date_add_reservation_should_fail()
        {
            var reservationDate = _now.AddDays(1);
            var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Black", "XYZ456", reservationDate);
            var nextReservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Black", "XYZ456", reservationDate);
            _weeklyParkingSpot.AddReservation(reservation, _now);

            var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(nextReservation, reservationDate));

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ParkingSpotAlreadyReservedException>();
        }

        [Fact]
        public void given_reservation_for_nottaken_date_add_reservation_should_succeed()
        {
            var reservationDate = _now.AddDays(1);
            var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "John Black", "XYZ456", reservationDate);

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
