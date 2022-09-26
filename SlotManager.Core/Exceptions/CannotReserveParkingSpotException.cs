using SlotManager.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotManager.Core.Exceptions
{
    public sealed class CannotReserveParkingSpotException : CustomException
    {
        public CannotReserveParkingSpotException(ParkingSpotId parkingSpotId) : base($"Cannot reserve parking spot with ID: {parkingSpotId}")
        {
            ParkingSpotId = parkingSpotId;
        }

        public ParkingSpotId ParkingSpotId { get; }
    }
}
