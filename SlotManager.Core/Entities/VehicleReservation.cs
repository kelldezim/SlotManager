using SlotManager.Core.ValueObjects;

namespace SlotManager.Core.Entities
{
    public sealed class VehicleReservation : Reservation
    {
        private VehicleReservation()
        {
        }

        public VehicleReservation(
            ReservationId id,
            ParkingSpotId parkingSpotId,
            Date date,
            EmployeeName employeeName,
            LicensePlate licensePlate)
            : base(id, parkingSpotId, date)
        {
            EmployeeName = employeeName;
            LicensePlate = ChangeLicencePlate(licensePlate);
        }

        public EmployeeName EmployeeName { get; private set; }
        public LicensePlate LicensePlate { get; private set; }

        public LicensePlate ChangeLicencePlate(LicensePlate licensePlate)
        {
            return LicensePlate = licensePlate;
        }
    }
}
