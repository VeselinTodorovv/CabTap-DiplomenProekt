using CabTap.Core.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CabTap.Data.Infrastructure;

public static class ApplicationBuilderExtension
{
    public static async Task PrepareDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var services = serviceScope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();
        
        await RoleSeeder(services);
        await SeedAdministratorUser(services);

        await SeedCategoriesAsync(context);
        await SeedManufacturersAsync(context);
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
            new Category { Name = "Standard", Rate = 1.2m },
            new Category { Name = "Premium", Rate = 2.0m},
            new Category { Name = "Luxury", Rate = 2.6m },
            new Category { Name = "Economy", Rate = 1.5m },
            new Category { Name = "Eco-friendly", Rate = 1.6m }
        );

        await context.SaveChangesAsync();
    }

    private static async Task SeedManufacturersAsync(ApplicationDbContext context)
    {
        if (context.Manufacturers.Any())
        {
            return;
        }
        
        context.Manufacturers.AddRange(
            new Manufacturer { Name = "Toyota" },
            new Manufacturer { Name = "BMW" },
            new Manufacturer { Name = "Mercedes" },
            new Manufacturer { Name = "Audi" },
            new Manufacturer { Name = "Volkswagen" },
            new Manufacturer { Name = "Volvo" },
            new Manufacturer { Name = "Honda" }
        );

        await context.SaveChangesAsync();
    }
}