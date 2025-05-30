using CabTap.Core.Entities;
using CabTap.Data;
using CabTap.Services.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var postgresConnectionString = config.GetConnectionString("PostgresConnection");
var dbPassword = Environment.GetEnvironmentVariable("CabTapDBPassword");

if (string.IsNullOrWhiteSpace(dbPassword))
{
    throw new InvalidOperationException("CabTapDBPassword is not set.");
}

var connBuilder = new NpgsqlConnectionStringBuilder(postgresConnectionString)
{
    Password = dbPassword
};

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options
        .UseLazyLoadingProxies()
        .UseNpgsql(connBuilder.ConnectionString,
            o => o.UseNetTopologySuite()));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequiredUniqueChars = 1;
        
        options.User.RequireUniqueEmail = true;
    })
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapperProfiles();

builder.Services.RegisterRepositories();
builder.Services.RegisterServices();

var app = builder.Build();

await app.PrepareDatabase();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();