using CabTap.Contracts.Services;
using CabTap.Shared.User;
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
        // Validate page number
        if (page <= 0)
        {
            return RedirectToAction(nameof(Index));
        }
        
        var users = await _userService.GetPaginatedClientsAsync(page, pageSize);
        
        var totalReservations = await _statisticService.CountClientsAsync();
        var totalPages = (int)Math.Ceiling((double)totalReservations / pageSize);
        // Ensure totalPages is at least 1
        if (totalPages <= 0)
        {
            totalPages = 1;
        }

        ViewData["CurrentPage"] = page;
        ViewData["TotalPages"] = totalPages;

        return View(users);
    }

    public async Task<IActionResult> ViewProfile(string id)
    {
        try
        {
            var clientDetails = await _userService.GetClientDetailsByIdAsync(id);

            return View(clientDetails);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    public async Task<IActionResult> DeleteProfile(string id)
    {
        try
        {
            var user = await _userService.GetClientToDeleteByIdAsync(id);

            return View(user);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProfile(ClientDeleteViewModel viewModel)
    {
        try
        {
            await _userService.DeleteClientAsync(viewModel.Id);
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
            return View(viewModel);
        }
    }
}