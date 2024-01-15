using CabTap.Shared.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace CabTap.Web.Controllers;

public class AccountController : Controller
{
    private readonly HttpClient _apiClient;
    
    public AccountController(IHttpClientFactory httpClientFactory)
    {
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
}