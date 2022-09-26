namespace SlotManager.Core.Exceptions
{
    public sealed class InvalidEmployeeName : CustomException
    {
        public InvalidEmployeeName() : base("Invalid Employee Name")
        {
        }
    }
}
