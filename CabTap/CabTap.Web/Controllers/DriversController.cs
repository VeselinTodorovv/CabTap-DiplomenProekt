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
    
    public Task<IActionResult> Create()
    {
        return Task.FromResult<IActionResult>(View());
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
        try
        {
            var driver = await _driverService.GetDriverByIdAsync(id);

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
        
        await _driverService.UpdateDriverAsync(viewModel);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var driver = await _driverService.GetDriverByIdAsync(id);

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
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(DriverDeleteViewModel viewModel)
    {
        await _driverService.DeleteDriverAsync(viewModel.Id);
        
        return RedirectToAction(nameof(Index));
    }
}