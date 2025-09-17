using CabTap.Core.Entities;
using CabTap.Core.Entities.Enums;
using CabTap.Data;
using Microsoft.AspNetCore.Identity;

namespace CabTap.Web.Extensions;

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
        
        await SeedDriversAsync(context);
        await SeedTaxisAsync(context);
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

        var adminUsername = Environment.GetEnvironmentVariable("ADMIN_USERNAME");
        var adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

        if (await userManager.FindByNameAsync(adminUsername) == null)
        {
            ApplicationUser user = new()
            {
                FirstName = adminUsername,
                LastName = adminUsername,
                UserName = adminUsername,
                Email = $"{adminUsername}@admin.com",
                Address = $"{adminUsername} address",
                PhoneNumber = "08888888"
            };

            var result = await userManager.CreateAsync(user, adminPassword);

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

        await context.Categories.AddRangeAsync(
            new Category { Name = "Standard", Rate = 1.2m, Image = "/images/categories/standard.png"},
            new Category { Name = "Premium", Rate = 2.0m, Image = "/images/categories/premium.png"},
            new Category { Name = "Luxury", Rate = 2.6m, Image = "/images/categories/luxury.png"},
            new Category { Name = "Economy", Rate = 1.5m, Image = "/images/categories/economy.png"},
            new Category { Name = "Eco-friendly", Rate = 1.6m, Image = "/images/categories/eco-friendly.png"}
        );

        await context.SaveChangesAsync();
    }

    private static async Task SeedManufacturersAsync(ApplicationDbContext context)
    {
        if (context.Manufacturers.Any())
        {
            return;
        }
        
        await context.Manufacturers.AddRangeAsync(
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
    
    private static async Task SeedDriversAsync(ApplicationDbContext context)
    {
        if (context.Drivers.Any())
        {
            return;
        }

        var dateTime = DateTime.UtcNow;
        await context.Drivers.AddRangeAsync(
            new Driver
            {
                Name = "John",
                CreatedBy = "admin",
                CreatedOn = dateTime,
                LastModifiedBy = "admin",
                LastModifiedOn = dateTime
            },
            new Driver
            {
                Name = "Jane",
                CreatedBy = "admin",
                CreatedOn = dateTime,
                LastModifiedBy = "admin",
                LastModifiedOn = dateTime
            }
        );
        
        await context.SaveChangesAsync();
    }

    private static async Task SeedTaxisAsync(ApplicationDbContext context)
    {
        if (context.Taxis.Any())
        {
            return;
        }

        var drivers = context.Drivers.Take(2).ToList();
        var categories = context.Categories.Take(3).ToList();

        var dateTime = DateTime.UtcNow;
        await context.Taxis.AddRangeAsync(
            new Taxi
            {
                RegNumber = "CA1234AA",
                DriverId = drivers[0].Id,
                CategoryId = categories[0].Id,
                TaxiStatus = TaxiStatus.Available,
                PassengerSeats = 4,
                ManufacturerId = 1,
                CreatedBy = "admin",
                CreatedOn = dateTime,
                LastModifiedBy = "admin",
                LastModifiedOn = dateTime,
            },
            new Taxi
            {
                RegNumber = "CA5678BB",
                DriverId = drivers[1].Id,
                CategoryId = categories[1].Id,
                TaxiStatus = TaxiStatus.Available,
                PassengerSeats = 4,
                ManufacturerId = 1,
                CreatedBy = "admin",
                CreatedOn = dateTime,
                LastModifiedBy = "admin",
                LastModifiedOn = dateTime
            }
        );
        
        await context.SaveChangesAsync();
    }
}