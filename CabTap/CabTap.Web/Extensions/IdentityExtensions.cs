using CabTap.Core.Entities;
using CabTap.Data;
using Microsoft.AspNetCore.Identity;

namespace CabTap.Web.Extensions;

public static class IdentityExtensions
{
    public static void AddApplicationIdentity(this IServiceCollection services)
    {
        services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
        
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
    }
}