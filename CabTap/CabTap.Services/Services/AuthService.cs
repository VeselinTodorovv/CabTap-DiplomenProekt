using System.Security.Claims;
using CabTap.Contracts.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace CabTap.Services.Services;

public class AuthService : IAuthService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public AuthService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task SignInUserAsync(string userName, string token)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.Name, userName),
            new (ClaimTypes.NameIdentifier, userName),
            new (ClaimTypes.Authentication, token)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromDays(30)),
            IsPersistent = true,
            AllowRefresh = true,
            IssuedUtc = DateTimeOffset.Now,
            RedirectUri = "/"
        };

        await _httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties
        );
    }

    public async Task SignOutUserAsync()
    {
        var authProperties = new AuthenticationProperties
        {
            IssuedUtc = DateTimeOffset.UtcNow,
            RedirectUri = "/"
        };

        await _httpContextAccessor.HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            authProperties
        );
    }
}