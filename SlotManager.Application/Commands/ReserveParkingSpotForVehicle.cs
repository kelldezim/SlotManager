using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotManager.Application.Commands
{
    public record ReserveParkingSpotForVehicle(Guid ParkingSpotId,
                                    Guid ReservationId,
                                    DateTime Date,
                                    string EmployeeName,
                                    string LicensePlate)
    {
    }
}
