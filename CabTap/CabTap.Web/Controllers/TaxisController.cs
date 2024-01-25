using CabTap.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CabTap.Web.Controllers;

public class TaxisController : Controller
{
    private readonly ITaxiService _taxiService;
    
    public TaxisController(ITaxiService taxiService)
    {
        _taxiService = taxiService;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }
    
    public async Task<IActionResult> Create()
    {
        return View();
    }
    
    public async Task<IActionResult> Details()
    {
        return View();
    }

    public async Task<IActionResult> Edit()
    {
        return View();
    }
    
    public async Task<IActionResult> Delete()
    {
        return View();
    }
}