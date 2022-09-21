using Microsoft.EntityFrameworkCore;
using SlotManager.Core.Entities;

namespace SlotManager.Infrastructure.DAL
{
    public class SlotManagerDbContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<WeeklyParkingSpot> WeeklyParkingSpots { get; set; }

        public SlotManagerDbContext(DbContextOptions<SlotManagerDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
    }
}
