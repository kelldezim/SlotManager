using Microsoft.Extensions.DependencyInjection;
using SlotManager.Application.Services;

namespace SlotManager.Application
{
    public static class Extensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IReservationService, ReservationService>();

            return services;
        }
    }
}
