﻿using SlotManager.Infrastructure.Time;
using System;

namespace SlotManager.Tests.Unit.Shared
{
    /// <summary>
    /// Design to get constant DateTime for tests persistance
    /// </summary>
    public class TestClock : IClock
    {
        public DateTime Current()
        {
            return new DateTime(2022, 08, 11);
        }
    }
}
