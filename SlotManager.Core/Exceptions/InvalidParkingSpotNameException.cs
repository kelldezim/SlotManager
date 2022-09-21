namespace SlotManager.Core.Exceptions
{
    public class InvalidParkingSpotNameException : CustomerException
    {
        public InvalidParkingSpotNameException() : base("Invalid Parking Spot Name")
        {
        }
    }
}
