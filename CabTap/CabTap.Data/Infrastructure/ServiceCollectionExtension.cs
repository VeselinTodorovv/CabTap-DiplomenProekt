using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Data.Repositories;
using CabTap.Services.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CabTap.Data.Infrastructure;

public static class ServiceCollectionExtension
{
    public static void ConfigureAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
            .AddCookie(options =>
            {
                options.Cookie.Name = "access_token";
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.SlidingExpiration = true;
        
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout"; 
            });
    }

    public static void ConfigureHttpSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var apiSettings = configuration.GetSection("ApiSettings");
        
        services.AddHttpContextAccessor();
        services.AddHttpClient("CabTapApi", client =>
        {
            client.BaseAddress = new Uri(apiSettings["BaseAddress"]);
            client.Timeout = TimeSpan.FromSeconds(30);
        });
    }

    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<ITaxiRepository, TaxiRepository>();
        services.AddTransient<IDriverRepository, DriverRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IReservationRepository, ReservationRepository>();

        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<ITaxiService, TaxiService>();
        services.AddTransient<IDriverService, DriverService>();
        services.AddTransient<ICategoryService, CategoryService>();
        services.AddTransient<IReservationService, ReservationService>();
    }
}