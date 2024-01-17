using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace CabTap.Data.Infrastructure;

public static class ServiceCollectionExtension
{
    private static IHttpClientFactory _httpClientFactory;
    
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

    public static void ConfigureHttpSettings(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddHttpClient("CabTapApi", client =>
        {
            client.BaseAddress = new Uri("https://localhost:7006/");
            client.Timeout = TimeSpan.FromSeconds(30);
        });
    }
}