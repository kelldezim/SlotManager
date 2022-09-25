using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SlotManager.Core.Repositories;
using SlotManager.Infrastructure.DAL.Repositories;

namespace SlotManager.Infrastructure.DAL
{
    internal static class Extensions
    {
        private const string SectionName = "sqlDataBase";
        public static IServiceCollection AddMSQL(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection(SectionName);
            services.Configure<SqlOptions>(section);
            var options = GetOptions<SqlOptions>(configuration, SectionName);

            var connectionString = options.ConnectionString;
            services.AddDbContext<SlotManagerDbContext>(x => x.UseSqlServer(connectionString));
            services.AddScoped<IWeeklyParkingSpotRepository, SqlWeeklyParkingSpotRepository>();
            services.AddHostedService<DatabaseInitializer>();

            return services;
        }

        public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
        {
            var options = new T();
            var section = configuration.GetSection(sectionName);
            section.Bind(options);

            return options;
        }
    }
}
