using SlotManager.Core.ValueObjects;

namespace SlotManager.Core.Entities
{
    public class Reservation
    {
        public Reservation(ReservationId id,
                           ParkingSpotId parkingSpotId,
                           EmployeeName employeeName,
                           LicensePlate licensePlate,
                           Date date)
        {
            Id = id;
            ParkingSpotId = parkingSpotId;
            EmployeeName = employeeName;
            ChangeLicencePlate(licensePlate);
            Date = date;
        }

        public ReservationId Id { get; private set; }
        public ParkingSpotId ParkingSpotId { get; private set; }
        public EmployeeName EmployeeName { get; private set; }
        public LicensePlate LicensePlate { get; private set; }
        public Date Date { get; private set; }

        public LicensePlate ChangeLicencePlate(LicensePlate licensePlate)
        {
            return LicensePlate = licensePlate;
        }

    }
}
