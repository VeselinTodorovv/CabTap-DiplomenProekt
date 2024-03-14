using System.Security.Claims;
using AutoMapper;

using CabTap.Contracts.Services;
using CabTap.Shared.Category;
using CabTap.Shared.Reservation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CabTap.Web.Controllers;

[Authorize(Roles = "Administrator, Client")]
public class ReservationsController : Controller
{
    private readonly IReservationService _reservationService;
    private readonly ITaxiService _taxiService;
    private readonly IStatisticService _statisticService;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public ReservationsController(IReservationService reservationService, ITaxiService taxiService, IMapper mapper, IStatisticService statisticService, ICategoryService categoryService)
    {
        _reservationService = reservationService;
        _taxiService = taxiService;
        _mapper = mapper;
        _statisticService = statisticService;
        _categoryService = categoryService;
    }

    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> Index(int page = 1, int pageSize = 9)
    {
        var reservations = await _reservationService.GetPaginatedReservationsAsync(page, pageSize);
        
        var totalReservations = await _statisticService.CountReservationsAsync();
        var totalPages = (int)Math.Ceiling((double)totalReservations / pageSize);

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;

        return View(reservations);
    }

    public async Task<IActionResult> MyReservations(int page = 1, int pageSize = 9)
    {
        var reservations = await _reservationService.GetPaginatedReservationsByUserIdAsync(page, pageSize);
        
        var totalReservations = await _statisticService.CountReservationsAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var totalPages = (int)Math.Ceiling((double)totalReservations / pageSize);

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = totalPages;
        
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
        var categories = await _taxiService.GetAvailableTaxiTypesAsync();

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
            return RedirectToAction(nameof(MyReservations));
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTotalPrice(int categoryId, double distance, double duration)
    {
        var categoryRate = (await _categoryService.GetCategoryByIdAsync(categoryId)).Rate;

        // Just an example
        var totalPrice = (decimal) distance * categoryRate;
        return Json(totalPrice);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkAsCompleted(string reservationId)
    {
        await _reservationService.MarkAsCompleted(reservationId);
        return RedirectToAction(nameof(MyReservations));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkAsCanceled(string reservationId)
    {
        await _reservationService.MarkAsCanceled(reservationId);
        return RedirectToAction(nameof(MyReservations));
    }

    public async Task<IActionResult> Edit(string id)
    {
        try
        {
            var reservation = await _reservationService.GetReservationByIdAsync(id);
            var availableCategories = (await _taxiService.GetAvailableTaxiTypesAsync()).ToList();

            // Add current category, if it is not already in the list
            if (availableCategories.All(c => c.Id != reservation.Taxi.CategoryId))
            {
                var currentCategory = new CategoryPairViewModel
                {
                    Id = reservation.Taxi.CategoryId,
                    Name = reservation.Taxi.Category.Name
                };
                availableCategories.Insert(0, currentCategory);
            }
            
            var model = _mapper.Map<ReservationEditViewModel>(reservation);

            model.Categories = availableCategories;
            model.CategoryId = reservation.Taxi.CategoryId;

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
    
    [Authorize(Roles = "Administrator")]
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
    
    [Authorize(Roles = "Administrator")]
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