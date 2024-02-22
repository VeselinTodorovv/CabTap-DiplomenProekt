using AutoMapper;
using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Data.Profiles;
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
        services.AddTransient<IManufacturerRepository, ManufacturerRepository>();

        services.AddTransient<ITaxiService, TaxiService>();
        services.AddTransient<IDriverService, DriverService>();
        services.AddTransient<IReservationService, ReservationService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IManufacturerService, ManufacturerService>();
        services.AddTransient<IUserService, UserService>();
    }

    public static void AddAutoMapperProfiles(this IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new DriverMappingProfile());
            cfg.AddProfile(new TaxiMappingProfile());
            cfg.AddProfile(new ReservationMappingProfile());
        });

        var mapper = config.CreateMapper();
        services.AddSingleton(mapper);
    }
}