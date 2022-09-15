namespace SlotManager.Api.Exceptions
{
    public abstract class CustomerException : Exception
    {
        public CustomerException(string msg) : base(msg)
        {

        }
    }
}
