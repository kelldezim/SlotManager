﻿namespace SlotManager.Infrastructure.Time
{
    public class Clock : IClock
    {
        public DateTime Current() => DateTime.UtcNow;
    }
}
