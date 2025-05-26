using AutoMapper;
using CabTap.Contracts.Repositories.Analytics;
using CabTap.Contracts.Repositories.Reservation;
using CabTap.Contracts.Repositories.Taxi;
using CabTap.Contracts.Services.Analytics;
using CabTap.Contracts.Services.Identity;
using CabTap.Contracts.Services.Reservation;
using CabTap.Contracts.Services.Taxi;
using CabTap.Contracts.Services.Utilities;
using CabTap.Data.Repositories;
using CabTap.Services.Profiles;
using CabTap.Services.Services.Analytics;
using CabTap.Services.Services.Identity;
using CabTap.Services.Services.Reservation;
using CabTap.Services.Services.Taxi;
using CabTap.Services.Services.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace CabTap.Services.Infrastructure;

public static class ServiceCollectionExtensions
{
    private static readonly Profile[] MappingProfiles =
    {
        new DriverMappingProfile(),
        new TaxiMappingProfile(),
        new ReservationMappingProfile(),
        new UserMappingProfile(),
        new CategoryMappingProfile()
    };

    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddTransient<ITaxiRepository, TaxiRepository>();
        services.AddTransient<IDriverRepository, DriverRepository>();
        services.AddTransient<IReservationRepository, ReservationRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IManufacturerRepository, ManufacturerRepository>();
        services.AddTransient<IStatisticRepository, StatisticRepository>();
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<ITaxiService, TaxiService>();
        services.AddTransient<IDriverService, DriverService>();
        services.AddTransient<IReservationService, ReservationService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IManufacturerService, ManufacturerService>();
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IStatisticService, StatisticService>();
        services.AddTransient<IDateTimeService, DateTimeService>();
        services.AddTransient<IAuditService, AuditService>();
        services.AddTransient<ITaxiManagerService, TaxiManagerService>();
    }

    public static void AddAutoMapperProfiles(this IServiceCollection services)
    {
        var config = new MapperConfiguration(cfg =>
        {
            AddProfiles(cfg, MappingProfiles);
        });

        var mapper = config.CreateMapper();
        services.AddSingleton(mapper);
    }

    private static void AddProfiles(IMapperConfigurationExpression cfg, Profile[] profiles)
    {
        foreach (var profile in profiles)
        {
            cfg.AddProfile(profile);
        }
    }
}