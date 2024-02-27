using AutoMapper;

using CabTap.Contracts.Services;
using CabTap.Shared.Reservation;
using Microsoft.AspNetCore.Mvc;

namespace CabTap.Web.Controllers;

public class ReservationsController : Controller
{
    private readonly IReservationService _reservationService;
    private readonly ITaxiService _taxiService;
    private readonly IMapper _mapper;
    
    public ReservationsController(IReservationService reservationService, ITaxiService taxiService, IMapper mapper)
    {
        _reservationService = reservationService;
        _taxiService = taxiService;
        _mapper = mapper;
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
            TaxiCategories = categories.ToList()
        };
        
        return View(reservationViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ReservationCreateViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }
        
        await _reservationService.AddReservationAsync(viewModel);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var reservation = await _reservationService.GetReservationByIdAsync(id);
        var availableCategories = await _taxiService.GetAvailableTaxiTypes();

        try
        {
            var model = _mapper.Map<ReservationEditViewModel>(reservation);

            model.TaxiCategories = availableCategories.ToList();

            return View(model);
        }
        catch (InvalidOperationException)
        {
            return NotFound(id);
        }
    }
}