using AutoMapper;
using CabTap.Contracts.Services.Analytics;
using CabTap.Contracts.Services.Taxi;
using CabTap.Shared.Driver;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CabTap.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class DriversController : Controller
{
    private readonly IDriverService _driverService;
    private readonly IStatisticService _statisticService;
    private readonly IMapper _mapper;
    
    public DriversController(IDriverService driverService, IMapper mapper, IStatisticService statisticService)
    {
        _driverService = driverService;
        _mapper = mapper;
        _statisticService = statisticService;
    }
    
    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        // Validate page number
        if (page <= 0)
        {
            return RedirectToAction(nameof(Index));
        }
        
        var drivers = await _driverService.GetPaginatedDriversAsync(page, pageSize);
        
        var totalReservations = await _statisticService.CountDriversAsync();
        var totalPages = (int)Math.Ceiling((double)totalReservations / pageSize);
        // Ensure totalPages is at least 1
        if (totalPages <= 0)
        {
            totalPages = 1;
        }
        
        ViewData["CurrentPage"] = page;
        ViewData["TotalPages"] = totalPages;
        
        return View(drivers);
    }
    
    public async Task<IActionResult> Details(string id)
    {
        try
        {
            var driver = await _driverService.GetDriverByIdAsync(id);

            return View(driver);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
    
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(DriverCreateViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        try
        {
            await _driverService.AddDriverAsync(viewModel);
            return RedirectToAction(nameof(Index));
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch (InvalidOperationException e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
            return View(viewModel);
        }
    }
    
    public async Task<IActionResult> Edit(string id)
    {
        try
        {
            var driver = await _driverService.GetDriverByIdAsync(id);

            var model = _mapper.Map<DriverEditViewModel>(driver);
        
            return View(model);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(DriverEditViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        try
        {
            await _driverService.UpdateDriverAsync(viewModel);
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
            return View(viewModel);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }

    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var driver = await _driverService.GetDriverByIdAsync(id);

            var model = _mapper.Map<DriverDeleteViewModel>(driver);
        
            return View(model);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(DriverDeleteViewModel viewModel)
    {
        try
        {
            await _driverService.DeleteDriverAsync(viewModel.Id);
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException e)
        {            
            ModelState.AddModelError(string.Empty, e.Message);
            return View(viewModel);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }
}