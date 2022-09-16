namespace SlotManager.Api.Exceptions
{
    public sealed class InvalidEmployeeName : CustomerException
    {
        public InvalidEmployeeName() : base("Invalid Employee Name")
        {
        }
    }
}
