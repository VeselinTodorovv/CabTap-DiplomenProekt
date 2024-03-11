using CabTap.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CabTap.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    private readonly IStatisticService _statisticService;

    public UsersController(IUserService userService, IStatisticService statisticService)
    {
        _userService = userService;
        _statisticService = statisticService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var users = await _userService.GetPaginatedClientsAsync(page, pageSize);
        
        var totalReservations = _statisticService.CountClients();
        var totalPages = (int)Math.Ceiling((double)totalReservations / pageSize);

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;

        return View(users);
    }

    public async Task<IActionResult> ViewProfile(string id)
    {
        try
        {
            var clientDetails = await _userService.GetClientDetailsAsync(id);

            return View(clientDetails);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}