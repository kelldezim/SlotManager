using Microsoft.Extensions.DependencyInjection;
using SlotManager.Core.Repositories;
using SlotManager.Infrastructure.DAL.Repositories;
using SlotManager.Infrastructure.Time;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SlotManager.Tests.Unit")]
namespace SlotManager.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IClock, Clock>();
            services.AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository>();

            return services;
        }
    }
}
