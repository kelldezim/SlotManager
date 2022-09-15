using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotManager.Api.Commands
{
    public record CreateReservation(Guid ParkingSpotId,
                                    Guid ReservationId,
                                    DateTime Date,
                                    string EmployeeName,
                                    string LicensePlate)
    {
    }
}
