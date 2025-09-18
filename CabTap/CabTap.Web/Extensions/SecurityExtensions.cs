namespace CabTap.Web.Extensions;

public static class SecurityExtensions
{
    public static void ConfigureCookies(this IServiceCollection services)
    {
        services.Configure<CookiePolicyOptions>(options =>
        {
            options.CheckConsentNeeded = _ => true;
            options.MinimumSameSitePolicy = SameSiteMode.Strict;
            options.Secure = CookieSecurePolicy.Always;
        });
    }

    public static void ConfigureHsts(this IServiceCollection services)
    {
        services.AddHsts(options =>
        {
            options.Preload = true;
            options.IncludeSubDomains = true;

            options.MaxAge = TimeSpan.FromDays(365);
        });
    }

    public static void ConfigureHttpsRedirection(this IServiceCollection services)
    {
        services.AddHttpsRedirection(options =>
        {
            options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
        });
    }
}