using CabTap.Contracts.Services;
using CabTap.Shared.Category;
using CabTap.Shared.Taxi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CabTap.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class TaxisController : Controller
{
    private readonly ITaxiService _taxiService;
    private readonly ICategoryService _categoryService;
    
    public TaxisController(ITaxiService taxiService, ICategoryService categoryService)
    {
        _taxiService = taxiService;
        _categoryService = categoryService;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var taxis = await _taxiService.GetAllAvailableTaxisAsync();
        
        return View(taxis);
    }
    
    public async Task<IActionResult> Details(int id)
    {
        var taxi = await _taxiService.GetTaxiByIdAsync(id);
        if (taxi == null)
        {
            return NotFound();
        }
        
        return View(taxi);
    }
    
    public async Task<IActionResult> Create()
    {
        var categories = await _categoryService.GetAllCategories();
        
        var taxi = new TaxiCreateViewModel
        {
            Categories = categories.Select(x => new CategoryPairViewModel
            {
                Id = x.Id,
                Name = x.Name
            })
                .ToList(),
            
            // TODO: Add Drivers
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
        var taxi = await _taxiService.GetTaxiByIdAsync(id);
        if (taxi == null)
        {
            return NotFound();
        }

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
        var taxi = await _taxiService.GetTaxiByIdAsync(id);
        if (taxi == null)
        {
            return NotFound();
        }

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
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(TaxiDeleteViewModel viewModel)
    {
        await _taxiService.DeleteTaxiAsync(viewModel.Id);
        
        return RedirectToAction(nameof(Index));
    }
}