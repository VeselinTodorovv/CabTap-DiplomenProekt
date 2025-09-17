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
}