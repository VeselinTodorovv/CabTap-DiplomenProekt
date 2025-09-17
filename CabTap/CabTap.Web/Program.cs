using CabTap.Data;
using CabTap.Web.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseExtensions(builder.Configuration);
builder.Services.AddApplicationIdentity();
builder.Services.AddApplicationServices();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = _ => true;
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
});


// TODO: Ensure CSP is effective against XSS attacks
// A strong Content Security Policy (CSP) significantly reduces the risk of cross-site scripting (XSS) attacks. Learn how to use a CSP to prevent XSS

// TODO: Use a strong HSTS policy
// Deployment of the HSTS header significantly reduces the risk of downgrading HTTP connections and eavesdropping attacks.
// A rollout in stages, starting with a low max-age is recommended.

// TODO: Ensure proper origin isolation with COOP
// The Cross-Origin-Opener-Policy (COOP) can be used to isolate the top-level window from other documents such as pop-ups.

// TODO: Mitigate clickjacking with XFO or CSP
// The X-Frame-Options (XFO) header or the frame-ancestors directive in the Content-Security-Policy (CSP) header control where a page can be embedded.
// These can mitigate clickjacking attacks by blocking some or all sites from embedding the page.

// TODO: Mitigate DOM-based XSS with Trusted Types
// The require-trusted-types-for directive in the Content-Security-Policy (CSP) header instructs user agents to control the data passed to DOM XSS sink functions.


var app = builder.Build();

// Apply pending Migrations
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
    
    await app.PrepareDatabase();
}

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

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();