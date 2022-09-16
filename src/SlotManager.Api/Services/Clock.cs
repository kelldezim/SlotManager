using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotManager.Api.Services
{
    public class Clock
    {
        public DateTime Current() => DateTime.UtcNow;
    }
}
