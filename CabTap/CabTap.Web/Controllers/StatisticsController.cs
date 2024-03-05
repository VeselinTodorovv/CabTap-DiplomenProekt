using CabTap.Contracts.Services;
using CabTap.Shared.Statistic;
using Microsoft.AspNetCore.Mvc;

namespace CabTap.Web.Controllers;

public class StatisticsController : Controller
{
    private readonly IStatisticService _statisticService;
    
    public StatisticsController(IStatisticService statisticService)
    {
        _statisticService = statisticService;
    }

    public IActionResult Index()
    {
        var statistics = new StatisticViewModel
        {
            CountClients = _statisticService.CountClients(),
            CountDrivers = _statisticService.CountDrivers(),
            CountReservations = _statisticService.CountReservations(),
            CountTaxis = _statisticService.CountTaxis(),
            SumReservations = _statisticService.SumReservations()
        };
        
        return View(statistics);
    }
}