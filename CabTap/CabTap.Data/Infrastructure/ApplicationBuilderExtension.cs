using CabTap.Core.Entities;
using CabTap.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public static class ApplicationBuilderExtension
{
    public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        var services = serviceScope.ServiceProvider;

        await SeedCategoriesAsync(services.GetRequiredService<ApplicationDbContext>());

        return app;
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