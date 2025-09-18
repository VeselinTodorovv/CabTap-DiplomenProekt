using CabTap.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CabTap.Web.Extensions;

public static class DatabaseExtensions
{
    public static void AddDatabaseExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        var postgresConnectionString = configuration.GetConnectionString("PostgresConnection");
        var dbPassword = configuration["CabTapDBPassword"];

        if (string.IsNullOrWhiteSpace(dbPassword))
        {
            throw new InvalidOperationException("CabTapDBPassword is not set.");
        }

        var connBuilder = new NpgsqlConnectionStringBuilder(postgresConnectionString)
        {
            Password = dbPassword
        };

        services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseLazyLoadingProxies()
                    .UseNpgsql(connBuilder.ConnectionString,
                    o => o.UseNetTopologySuite()))
            .AddHealthChecks()
            .AddNpgSql(connBuilder.ConnectionString);
    }

    public static async Task ApplyPendingMigrationsAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        if ((await context.Database.GetPendingMigrationsAsync()).Any())
        {
            await context.Database.MigrateAsync();
        }
    }
}