using CabTap.Contracts.Services;
using CabTap.Shared.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CabTap.Web.Controllers;

public class AccountController : Controller
{
    private readonly HttpClient _apiClient;
    private readonly IAuthService _authService;
    
    public AccountController(IAuthService authService, IHttpClientFactory httpClientFactory)
    {
        _authService = authService;
        _apiClient = httpClientFactory.CreateClient("CabTapApi");
    }

    // GET
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var registerResponse = await _apiClient.PostAsJsonAsync("auth/register", model);

        if (!registerResponse.IsSuccessStatusCode)
        {
            ModelState.AddModelError("", "Registration failed. Please try again.");
            return View(model);
        }

        return RedirectToAction("Index", "Home");
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        
        var response = await _apiClient.PostAsJsonAsync("auth/login", model);

        if (!response.IsSuccessStatusCode)
        {
            ModelState.AddModelError(string.Empty, "Login failed");

            return View(model);
        }
        
        var token = await response.Content.ReadAsStringAsync();
        await _authService.SignInUserAsync(model.UserName, token);

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        if (User.Identity is {IsAuthenticated: true})
        {
            await _authService.SignOutUserAsync();
        }

        return RedirectToAction("Index", "Home");
    }
}