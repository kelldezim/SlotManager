using SlotManager.Api.Exceptions;

namespace SlotManager.Api.Entities
{
    public class Reservation
    {
        public Reservation(Guid id, Guid parkingSpotId, string employeeName, string licensePlate, DateTime date)
        {
            Id = id;
            ParkingSpotId = parkingSpotId;
            EmployeeName = employeeName;
            ChangeLicencePlate(licensePlate);
            Date = date;
        }

        public Guid Id { get; }
        public Guid ParkingSpotId { get; private set; }
        public string EmployeeName { get; private set; }
        public string LicensePlate { get; private set; }
        public DateTime Date { get; private set; }

        public void ChangeLicencePlate(string newLicencePlate)
        {
            if (string.IsNullOrWhiteSpace(newLicencePlate))
            {
                throw new EmptyLicencePlateException();
            }

            LicensePlate = newLicencePlate;
        }

    }
}
