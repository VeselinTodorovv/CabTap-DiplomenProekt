using CabTap.Contracts.Services.Analytics;
using CabTap.Shared.Statistic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CabTap.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class StatisticsController : Controller
{
    private readonly IStatisticService _statisticService;
    
    public StatisticsController(IStatisticService statisticService)
    {
        _statisticService = statisticService;
    }

    public async Task<IActionResult> Index()
    {
        var statistics = await GetStatisticsAsync();
        
        return View(statistics);
    }

    private async Task<StatisticViewModel> GetStatisticsAsync()
    {
        return new StatisticViewModel
        {
            CountClients = await _statisticService.CountClientsAsync(),
            CountDrivers = await _statisticService.CountDriversAsync(),
            CountReservations = await _statisticService.CountReservationsAsync(),
            CountTaxis = await _statisticService.CountTaxisAsync(),
            SumReservations = await _statisticService.SumReservationsAsync()
        };
    }
}