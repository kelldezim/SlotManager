namespace SlotManager.Core.Exceptions
{
    public sealed class EmptyLicencePlateException : CustomerException
    {
        public EmptyLicencePlateException() : base("Licence plate is empty")
        {
        }
    }
}
