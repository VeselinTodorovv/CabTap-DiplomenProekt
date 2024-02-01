using CabTap.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace CabTap.Api.Infrastructure;

internal static class ApplicationBuilderExtension
{
    internal static async Task PrepareDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        
        var services = scope.ServiceProvider;
        
        await RoleSeeder(services);
        await SeedAdministratorUser(services);
    }
    
    private static async Task RoleSeeder(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        string[] roles = { "Administrator", "Client" };

        foreach (var role in roles)
        {
            var roleExists = await roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
    
    private static async Task SeedAdministratorUser(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (await userManager.FindByNameAsync("admin") == null)
        {
            ApplicationUser user = new()
            {
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin",
                Email = "admin@admin.com",
                Address = "admin address",
                PhoneNumber = "08888888"
            };

            var result = await userManager.CreateAsync(user, "@Admin12315");

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "Administrator");
            }
        }
    }
}