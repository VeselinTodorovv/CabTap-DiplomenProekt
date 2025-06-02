using AutoMapper;
using CabTap.Contracts.Repositories.Analytics;
using CabTap.Contracts.Repositories.Reservation;
using CabTap.Contracts.Repositories.Taxi;
using CabTap.Contracts.Services.Analytics;
using CabTap.Contracts.Services.Identity;
using CabTap.Contracts.Services.Reservation;
using CabTap.Contracts.Services.Taxi;
using CabTap.Contracts.Services.Utilities;
using CabTap.Data.Repositories.Analytics;
using CabTap.Data.Repositories.Reservation;
using CabTap.Data.Repositories.Taxi;
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
        services.AddScoped<ITaxiRepository, TaxiRepository>();
        services.AddScoped<IDriverRepository, DriverRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IManufacturerRepository, ManufacturerRepository>();
        services.AddScoped<IStatisticRepository, StatisticRepository>();
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ITaxiService, TaxiService>();
        services.AddScoped<IDriverService, DriverService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IManufacturerService, ManufacturerService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IStatisticService, StatisticService>();
        services.AddScoped<IDateTimeService, DateTimeService>();
        services.AddScoped<IAuditService, AuditService>();
        services.AddScoped<ITaxiManagerService, TaxiManagerService>();
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