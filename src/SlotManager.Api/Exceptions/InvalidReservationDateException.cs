namespace SlotManager.Api.Exceptions
{
    public class InvalidReservationDateException : CustomerException
    {
        public DateTime Date { get; }
        public InvalidReservationDateException(DateTime date) : base($"Invalid reservation date: {date:d}")
        {
            Date = date;
        }
    }
}
