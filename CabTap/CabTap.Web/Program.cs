using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Data;
using CabTap.Data.Infrastructure;
using CabTap.Data.Repositories;
using CabTap.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddTransient<ITaxiRepository, TaxiRepository>();
builder.Services.AddTransient<IDriverRepository, DriverRepository>();

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<ITaxiService, TaxiService>();
builder.Services.AddTransient<IDriverService, DriverService>();

// HTTP Settings
builder.Services.ConfigureHttpSettings();
builder.Services.ConfigureAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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