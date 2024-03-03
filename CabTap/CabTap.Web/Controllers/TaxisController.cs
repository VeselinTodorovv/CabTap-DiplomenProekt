using AutoMapper;
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
    private readonly IMapper _mapper;
    
    public TaxisController(ITaxiService taxiService, ICategoryService categoryService, IManufacturerService manufacturerService, IDriverService driverService, IMapper mapper)
    {
        _taxiService = taxiService;
        _categoryService = categoryService;
        _manufacturerService = manufacturerService;
        _driverService = driverService;
        _mapper = mapper;
    }

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

        try
        {
            await _taxiService.AddTaxiAsync(viewModel);
            return RedirectToAction(nameof(Index));
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var taxi = await _taxiService.GetTaxiByIdAsync(id);
            
            var categories = await _categoryService.GetAllCategories();
            var manufacturers = await _manufacturerService.GetAllManufacturers();
            var drivers = await _driverService.GetAllDriversAsync();
            
            var model = _mapper.Map<TaxiEditViewModel>(taxi);
            
            model.Manufacturers = manufacturers.ToList();
            model.Categories = categories.ToList();
            model.Drivers = drivers.Select(x => new DriverPairViewModel
            {
                Id = x.Id,
                Name = x.Name
            })
                .ToList();
        
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
        
        try
        {
            await _taxiService.UpdateTaxiAsync(viewModel);
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
    
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var taxi = await _taxiService.GetTaxiByIdAsync(id);

            var model = _mapper.Map<TaxiDeleteViewModel>(taxi);
        
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
        try
        {
            await _taxiService.DeleteTaxiAsync(viewModel.Id);
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