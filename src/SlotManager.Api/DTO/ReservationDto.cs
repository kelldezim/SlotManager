using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotManager.Api.DTO
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        public Guid ParkingSpotId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime Date { get; set; }
    }
}
