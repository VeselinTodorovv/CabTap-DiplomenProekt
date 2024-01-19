using CabTap.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CabTap.Web.Controllers;

public class DriversController : Controller
{
    private readonly IDriverService _driverService;
    
    public DriversController(IDriverService driverService)
    {
        _driverService = driverService;
    }
}