using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotManager.Api.Exceptions
{
    public class InvalidParkingSpotNameException : CustomerException
    {
        public InvalidParkingSpotNameException() : base("Invalid Parking Spot Name")
        {
        }
    }
}
