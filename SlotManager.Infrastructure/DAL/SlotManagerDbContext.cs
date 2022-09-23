using Microsoft.EntityFrameworkCore;
using SlotManager.Core.Entities;

namespace SlotManager.Infrastructure.DAL
{
    internal class SlotManagerDbContext : DbContext
    {
        //const string _connectionString = "Data Source=LAPTOP-KHD0U7M8\\SQLEXPRESS;Initial Catalog = SlotManager; Integrated Security = True";
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<WeeklyParkingSpot> WeeklyParkingSpots { get; set; }

        public SlotManagerDbContext(DbContextOptions<SlotManagerDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //IEntityTypeConfiguration
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_connectionString);
        //}  
    }
}
