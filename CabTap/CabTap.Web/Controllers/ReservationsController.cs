using AutoMapper;

using CabTap.Contracts.Services;
using CabTap.Shared.Category;
using CabTap.Shared.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CabTap.Web.Controllers;

[Authorize(Roles = "Administrator")]
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

    public async Task<IActionResult> Details(string id)
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

        try
        {
            await _reservationService.AddReservationAsync(viewModel);
            return RedirectToAction(nameof(Index));
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }

    public async Task<IActionResult> Edit(string id)
    {
        try
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            var availableCategories = (await _taxiService.GetAvailableTaxiTypes()).ToList();

            // Add current category, if it is not already in the list
            if (availableCategories.All(c => c.Id != reservation.Taxi.CategoryId))
            {
                var currentCategory = new CategoryPairViewModel
                {
                    Id = reservation.Taxi.CategoryId,
                    Name = reservation.Taxi.Category.Name
                };
                availableCategories.Add(currentCategory);
            }
            
            var model = _mapper.Map<ReservationEditViewModel>(reservation);

            model.Categories = availableCategories;

            return View(model);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ReservationEditViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }
        
        try
        {
            await _reservationService.UpdateReservationAsync(viewModel);
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
    
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);

            var model = _mapper.Map<ReservationDeleteViewModel>(reservation);
        
            return View(model);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(ReservationDeleteViewModel viewModel)
    {
        try
        {
            await _reservationService.DeleteReservationAsync(viewModel.Id);
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