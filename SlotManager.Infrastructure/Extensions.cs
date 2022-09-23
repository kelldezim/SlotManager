using Microsoft.Extensions.DependencyInjection;
using SlotManager.Infrastructure.DAL;
using SlotManager.Infrastructure.Time;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SlotManager.Tests.Unit")]
namespace SlotManager.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services
                .AddMSQL()
                .AddSingleton<IClock, Clock>();
                //.AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository>()              

            return services;
        }
    }
}
