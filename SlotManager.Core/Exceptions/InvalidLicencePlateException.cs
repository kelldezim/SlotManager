namespace SlotManager.Core.Exceptions
{
    public sealed class InvalidLicencePlateException : CustomException
    {
        public string LicensePlate { get; set; }
        public InvalidLicencePlateException(string licensePlate) : base($"Licence plate: {licensePlate} is invalid.")
        {
            LicensePlate = licensePlate;
        }
    }
}
