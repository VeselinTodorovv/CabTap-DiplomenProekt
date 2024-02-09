using CabTap.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CabTap.Data.Infrastructure;

public static class ApplicationBuilderExtension
{
    public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var services = serviceScope.ServiceProvider;
        
        await RoleSeeder(services);
        await SeedAdministratorUser(services);

        await SeedCategoriesAsync(services.GetRequiredService<ApplicationDbContext>());

        return app;
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

    private static async Task SeedCategoriesAsync(ApplicationDbContext context)
    {
        if (context.Categories.Any())
        {
            return;
        }

        context.Categories.AddRange(
            new Category { Name = "Standard" },
            new Category { Name = "Premium" },
            new Category { Name = "Luxury" },
            new Category { Name = "Economy" },
            new Category { Name = "Eco-friendly" },
            new Category { Name = "Business" }
        );

        await context.SaveChangesAsync();
    }
}