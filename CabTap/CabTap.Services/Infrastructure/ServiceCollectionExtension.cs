using AutoMapper;
using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Data.Repositories;
using CabTap.Services.Profiles;
using CabTap.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CabTap.Services.Infrastructure;

public static class ServiceCollectionExtension
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<ITaxiRepository, TaxiRepository>();
        services.AddTransient<IDriverRepository, DriverRepository>();
        services.AddTransient<IReservationRepository, ReservationRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IManufacturerRepository, ManufacturerRepository>();
        services.AddTransient<IStatisticRepository, StatisticRepository>();

        services.AddTransient<ITaxiService, TaxiService>();
        services.AddTransient<IDriverService, DriverService>();
        services.AddTransient<IReservationService, ReservationService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IManufacturerService, ManufacturerService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IStatisticService, StatisticService>();
        services.AddTransient<IDateTimeService, DateTimeService>();
    }

    public static void AddAutoMapperProfiles(this IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new DriverMappingProfile());
            cfg.AddProfile(new TaxiMappingProfile());
            cfg.AddProfile(new ReservationMappingProfile());
            cfg.AddProfile(new UserMappingProfile());
        });

        var mapper = config.CreateMapper();
        services.AddSingleton(mapper);
    }
}