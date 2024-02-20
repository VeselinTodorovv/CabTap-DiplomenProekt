using CabTap.Contracts.Services;
using CabTap.Shared.Reservation;
using Microsoft.AspNetCore.Mvc;

namespace CabTap.Web.Controllers;

public class ReservationsController : Controller
{
    private readonly IReservationService _reservationService;
    private readonly ITaxiService _taxiService;
    
    public ReservationsController(IReservationService reservationService, ITaxiService taxiService)
    {
        _reservationService = reservationService;
        _taxiService = taxiService;
    }

    public async Task<IActionResult> Index()
    {
        var reservations = await _reservationService.GetAllReservationsAsync();

        return View(reservations);
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
        
            return View(reservation);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
    
    public async Task<IActionResult> Create()
    {
        var categories = await _taxiService.GetAvailableTaxiTypes();

        var reservationViewModel = new ReservationCreateViewModel
        {
            TaxiCategories = categories
        };
        
        return View(reservationViewModel);
    }
}