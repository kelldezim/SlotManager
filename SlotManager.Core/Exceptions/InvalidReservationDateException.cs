﻿namespace SlotManager.Core.Exceptions
{
    public class InvalidReservationDateException : CustomException
    {
        public DateTime Date { get; }
        public InvalidReservationDateException(DateTime date) : base($"Invalid reservation date: {date:d}")
        {
            Date = date;
        }
    }
}
