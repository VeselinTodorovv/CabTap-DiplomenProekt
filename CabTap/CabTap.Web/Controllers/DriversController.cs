using CabTap.Contracts.Services;
using CabTap.Shared.Driver;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CabTap.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class DriversController : Controller
{
    private readonly IDriverService _driverService;
    
    public DriversController(IDriverService driverService)
    {
        _driverService = driverService;
    }
    
    public async Task<IActionResult> Index()
    {
        var drivers = await _driverService.GetAllDriversAsync();
        
        return View(drivers);
    }
    
    public async Task<IActionResult> Details(string id)
    {
        var driver = await _driverService.GetDriverByIdAsync(id);
        if (driver == null)
        {
            return NotFound();
        }

        return View(driver);
    }
    
    public async Task<IActionResult> Create()
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
        
        await _driverService.AddDriverAsync(viewModel);
        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Edit(string id)
    {
        var driver = await _driverService.GetDriverByIdAsync(id);
        if (driver == null)
        {
            return NotFound();
        }

        var model = new DriverEditViewModel
        {
            Id = driver.Id,
            Name = driver.Name,
            CreatedBy = driver.CreatedBy,
            CreatedOn = driver.CreatedOn,
            LastModifiedBy = driver.LastModifiedBy,
            LastModifiedOn = driver.LastModifiedOn
        };
        
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(DriverEditViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }
        
        await _driverService.UpdateDriverAsync(viewModel);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(string id)
    {
        var driver = await _driverService.GetDriverByIdAsync(id);
        if (driver == null)
        {
            return NotFound();
        }

        var model = new DriverDeleteViewModel
        {
            Id = driver.Id,
            Name = driver.Name,
            CreatedBy = driver.CreatedBy,
            CreatedOn = driver.CreatedOn,
            LastModifiedBy = driver.LastModifiedBy,
            LastModifiedOn = driver.LastModifiedOn
        };
        
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(DriverDeleteViewModel viewModel)
    {
        await _driverService.DeleteDriverAsync(viewModel.Id);
        
        return RedirectToAction(nameof(Index));
    }
}