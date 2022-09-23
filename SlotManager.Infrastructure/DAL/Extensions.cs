using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SlotManager.Core.Repositories;
using SlotManager.Infrastructure.DAL.Repositories;

namespace SlotManager.Infrastructure.DAL
{
    internal static class Extensions
    {
        public static IServiceCollection AddMSQL(this IServiceCollection services)
        {
            const string connectionString = "Data Source=LAPTOP-KHD0U7M8\\SQLEXPRESS;Initial Catalog = SlotManager; Integrated Security = True";
            services.AddDbContext<SlotManagerDbContext>(x => x.UseSqlServer(connectionString));
            services.AddScoped<IWeeklyParkingSpotRepository, SqlWeeklyParkingSpotRepository>();
            services.AddHostedService<DatabaseInitializer>();

            return services;
        }
    }
}
