namespace SlotManager.Core.Exceptions
{
    public sealed class InvalidEmployeeName : CustomerException
    {
        public InvalidEmployeeName() : base("Invalid Employee Name")
        {
        }
    }
}
