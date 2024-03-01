using AutoMapper;
using CabTap.Contracts.Services;
using CabTap.Shared.Driver;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CabTap.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class DriversController : Controller
{
    private readonly IDriverService _driverService;
    private readonly IMapper _mapper;
    
    public DriversController(IDriverService driverService, IMapper mapper)
    {
        _driverService = driverService;
        _mapper = mapper;
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

        try
        {
            await _driverService.AddDriverAsync(viewModel);
            return RedirectToAction(nameof(Index));
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
    }
}