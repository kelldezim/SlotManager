using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SlotManager.Core.ValueObjects;
using SlotManager.Infrastructure.Time;

namespace SlotManager.Infrastructure.DAL
{
    internal sealed class DatabaseInitializer : IHostedService
    {
        //two options:
        //IHostedService do sth on start and do sth on end of app run
        //BackgroundService for task that need to be perform in the meantime of application run

        private readonly IServiceProvider _serviceProvider;

        public DatabaseInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //check migrations and auto apply them also db
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<SlotManagerDbContext>();
                dbContext.Database.Migrate();

                var weeklyParkingSpots = dbContext.WeeklyParkingSpots.ToList();
                if (weeklyParkingSpots.Any())
                {
                    return Task.CompletedTask;
                }

                var clock = new Clock();

                weeklyParkingSpots = new()
                {
                    new(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(clock.Current()), "P1"),
                    new(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(clock.Current()), "P2"),
                    new(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(clock.Current()), "P3"),
                    new(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(clock.Current()), "P4"),
                    new(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(clock.Current()), "P5")
                };

                dbContext.WeeklyParkingSpots.AddRange(weeklyParkingSpots);
                dbContext.SaveChanges();


            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
