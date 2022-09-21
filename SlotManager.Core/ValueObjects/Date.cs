﻿namespace SlotManager.Core.ValueObjects
{
    public record Date
    {
        public DateTimeOffset Value { get; set; }

        public Date(DateTimeOffset value)
        {
            Value = value;
        }

        public Date AddDays(int days) => new Date(Value.AddDays(days));

        public static implicit operator DateTimeOffset(Date date) => date.Value;

        public static implicit operator Date(DateTimeOffset value) => new Date(value);

        public static bool operator < (Date date1, Date date2)
        {
            return date1.Value < date2.Value;
        }

        public static bool operator > (Date date1, Date date2)
        {
            return date1.Value > date2.Value;
        }

        public static bool operator <= (Date date1, Date date2)
        {
            return date1.Value <= date2.Value;
        }

        public static bool operator >= (Date date1, Date date2)
        {
            return date1.Value <= date2.Value;
        }

        public static Date Now => new Date(DateTimeOffset.Now);

        public override string ToString() => Value.ToString("d");
    }
}
