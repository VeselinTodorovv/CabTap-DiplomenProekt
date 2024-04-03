using AutoMapper;
using CabTap.Contracts.Services;
using CabTap.Shared.Driver;
using CabTap.Shared.Taxi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CabTap.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class TaxisController : Controller
{
    private readonly ITaxiService _taxiService;
    
    private readonly ICategoryService _categoryService;
    private readonly IManufacturerService _manufacturerService;
    private readonly IDriverService _driverService;
    private readonly IMapper _mapper;
    private readonly IStatisticService _statisticService;
    
    public TaxisController(ITaxiService taxiService, ICategoryService categoryService, IManufacturerService manufacturerService, IDriverService driverService, IMapper mapper, IStatisticService statisticService)
    {
        _taxiService = taxiService;
        _categoryService = categoryService;
        _manufacturerService = manufacturerService;
        _driverService = driverService;
        _mapper = mapper;
        _statisticService = statisticService;
    }

    public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
    {
        var taxis = await _taxiService.GetPaginatedTaxisAsync(page, pageSize);
        
        var totalReservations = await _statisticService.CountTaxisAsync();
        var totalPages = (int)Math.Ceiling((double)totalReservations / pageSize);
        
        ViewData["CurrentPage"] = page;
        ViewData["TotalPages"] = totalPages;

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
        var categories = await _categoryService.GetAllCategoriesAsync();
        var manufacturers = await _manufacturerService.GetAllManufacturersAsync();
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
        catch (InvalidOperationException e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
            return View(viewModel);
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(string.Empty, "Taxi with this registration number already exists");
            return View(viewModel);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var taxi = await _taxiService.GetTaxiByIdAsync(id);
            
            var categories = await _categoryService.GetAllCategoriesAsync();
            var manufacturers = await _manufacturerService.GetAllManufacturersAsync();
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