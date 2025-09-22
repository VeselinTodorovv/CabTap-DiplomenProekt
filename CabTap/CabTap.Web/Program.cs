using CabTap.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseExtensions(builder.Configuration);
builder.Services.AddApplicationIdentity();
builder.Services.AddApplicationServices();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.ConfigureHsts();
builder.Services.ConfigureHttpsRedirection();
builder.Services.ConfigureCookies();
builder.Services.ConfigureAntiforgery();

var app = builder.Build();

await app.Services.ApplyPendingMigrationsAsync();
await app.PrepareDatabase();

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
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

app.UseCookiePolicy();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();