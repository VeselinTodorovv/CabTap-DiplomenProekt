using CabTap.Contracts.Services;
using CabTap.Shared.Driver;
using CabTap.Shared.Taxi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CabTap.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class TaxisController : Controller
{
    private readonly ITaxiService _taxiService;
    
    private readonly ICategoryService _categoryService;
    private readonly IManufacturerService _manufacturerService;
    private readonly IDriverService _driverService;
    
    public TaxisController(ITaxiService taxiService, ICategoryService categoryService, IManufacturerService manufacturerService, IDriverService driverService)
    {
        _taxiService = taxiService;
        _categoryService = categoryService;
        _manufacturerService = manufacturerService;
        _driverService = driverService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var taxis = await _taxiService.GetAllTaxisAsync();
        
        return View(taxis);
    }
    
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var taxi = await _taxiService.GetTaxiByIdAsync(id);
        
            return View(taxi);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
    
    public async Task<IActionResult> Create()
    {
        var categories = await _categoryService.GetAllCategories();
        var manufacturers = await _manufacturerService.GetAllManufacturers();
        var drivers = await _driverService.GetAllDriversAsync();
        
        var taxi = new TaxiCreateViewModel
        {
            Categories = categories.ToList(),
            
            Manufacturers = manufacturers.ToList(),
            
            Drivers = drivers.Select(x => new DriverPairViewModel
            {
                Id = x.Id,
                Name = x.Name
            })
                .ToList(),
        };
        
        return View(taxi);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TaxiCreateViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }
        
        await _taxiService.AddTaxiAsync(viewModel);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var taxi = await _taxiService.GetTaxiByIdAsync(id);
            
            var model = new TaxiEditViewModel
            {
                Id = taxi.Id,
                RegNumber = taxi.RegNumber,
                ManufacturerId = taxi.ManufacturerId,
                CategoryId = taxi.CategoryId,
                DriverId = taxi.DriverId,
                Description = taxi.Description,
                Picture = taxi.Picture,
                TaxiStatus = taxi.TaxiStatus,
                PassengerSeats = taxi.PassengerSeats,
            
                CreatedBy = taxi.CreatedBy,
                CreatedOn = taxi.CreatedOn,
                LastModifiedBy = taxi.LastModifiedBy,
                LastModifiedOn = taxi.LastModifiedOn,
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
    public async Task<IActionResult> Edit(TaxiEditViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }
        
        await _taxiService.UpdateTaxiAsync(viewModel);
        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var taxi = await _taxiService.GetTaxiByIdAsync(id);

            var model = new TaxiDeleteViewModel
            {
                Id = taxi.Id,
                RegNumber = taxi.RegNumber,
                ManufacturerId = taxi.ManufacturerId,
                CategoryId = taxi.CategoryId,
                DriverId = taxi.DriverId,
                Description = taxi.Description,
                Picture = taxi.Picture,
                TaxiStatus = taxi.TaxiStatus,
                PassengerSeats = taxi.PassengerSeats,
            
                CreatedBy = taxi.CreatedBy,
                CreatedOn = taxi.CreatedOn,
                LastModifiedBy = taxi.LastModifiedBy,
                LastModifiedOn = taxi.LastModifiedOn,
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
    public async Task<IActionResult> Delete(TaxiDeleteViewModel viewModel)
    {
        await _taxiService.DeleteTaxiAsync(viewModel.Id);
        
        return RedirectToAction(nameof(Index));
    }
}