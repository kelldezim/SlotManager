using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SlotManager.Infrastructure.DAL;
using SlotManager.Infrastructure.Time;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("SlotManager.Tests.Unit")]
namespace SlotManager.Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("app");
            services.Configure<AppOptions>(section);

            services
                .AddMSQL(configuration)
                .AddSingleton<IClock, Clock>();
                //.AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository>()              

            return services;
        }
    }
}
