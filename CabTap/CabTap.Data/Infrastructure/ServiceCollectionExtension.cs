using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Data.Repositories;
using CabTap.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CabTap.Data.Infrastructure;

public static class ServiceCollectionExtension
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<ITaxiRepository, TaxiRepository>();
        services.AddTransient<IDriverRepository, DriverRepository>();
        services.AddTransient<IReservationRepository, ReservationRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();

        services.AddTransient<ITaxiService, TaxiService>();
        services.AddTransient<IDriverService, DriverService>();
        services.AddTransient<IReservationService, ReservationService>();
        services.AddTransient<ICategoryService, CategoryService>();
    }
}