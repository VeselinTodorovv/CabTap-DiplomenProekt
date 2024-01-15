using CabTap.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CabTap.Web.Controllers;

public class TaxiController : Controller
{
    private readonly ITaxiService _taxiService;
    
    public TaxiController(ITaxiService taxiService)
    {
        _taxiService = taxiService;
    }
}