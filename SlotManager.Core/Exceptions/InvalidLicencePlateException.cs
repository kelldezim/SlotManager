namespace SlotManager.Core.Exceptions
{
    public sealed class InvalidLicencePlateException : CustomerException
    {
        public string LicensePlate { get; set; }
        public InvalidLicencePlateException(string licensePlate) : base($"Licence plate: {licensePlate} is invalid.")
        {
            LicensePlate = licensePlate;
        }
    }
}
