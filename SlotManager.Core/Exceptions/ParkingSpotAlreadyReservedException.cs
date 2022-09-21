namespace SlotManager.Core.Exceptions
{
    public class ParkingSpotAlreadyReservedException : CustomerException
    {
        public ParkingSpotAlreadyReservedException(DateTime dateTime, string name) : base($"Parking spot: {name} is already reserved at: {dateTime:d}.")
        {
            DateTime = dateTime;
            Name = name;
        }

        public DateTime DateTime { get; }
        public string Name { get; }
    }
}
