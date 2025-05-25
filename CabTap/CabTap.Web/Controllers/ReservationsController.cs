using AutoMapper;
using CabTap.Contracts.Services.Analytics;
using CabTap.Contracts.Services.Reservation;
using CabTap.Contracts.Services.Taxi;
using CabTap.Core.Entities.Enums;
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
    public async Task<IActionResult> Index(string searchInput, string sortOption, string reservationType, int page = 1, int pageSize = 9)
    {
        // Validate page number
        if (page <= 0)
        {
            return RedirectToAction(nameof(Index));
        }
        
        var reservations = await _reservationService.GetPaginatedReservationsAsync(searchInput, sortOption, reservationType, page, pageSize);
        
        var totalReservations = string.IsNullOrWhiteSpace(searchInput)
            ? await _statisticService.CountReservationsAsync()
            : await _statisticService.CountReservationsAsync(searchInput);

        var totalPages = (int)Math.Ceiling((double)totalReservations / pageSize);
        // Ensure totalPages is at least 1
        if (totalPages <= 0)
        {
            totalPages = 1;
        }

        ViewData["CurrentPage"] = page;
        ViewData["TotalPages"] = totalPages;

        return View(reservations);
    }

    public async Task<IActionResult> MyReservations(string searchInput, string sortOption, string reservationType, int page = 1, int pageSize = 9)
    {
        // Validate page number
        if (page <= 0)
        {
            return RedirectToAction(nameof(Index));
        }
        
        var reservations = await _reservationService
            .GetPaginatedReservationsByUserNameAsync(searchInput, sortOption,reservationType, page, pageSize);
        
        var userName = User.Identity!.Name;
        if (userName == null)
        {
            return View(reservations);
        }
        
        var totalReservations = await _statisticService.CountReservationsAsync(userName);
        var totalPages = (int)Math.Ceiling((double)totalReservations / pageSize);
        
        // Ensure totalPages is at least 1
        if (totalPages <= 0)
        {
            totalPages = 1;
        }

        ViewData["CurrentPage"] = page;
        ViewData["TotalPages"] = totalPages;
        
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
            TaxiCategories = categories.ToList(),
            ReservationDateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, DateTime.Today.Hour, 0, 0, DateTimeKind.Local),
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
        catch (InvalidOperationException e)
        {
            ModelState.AddModelError(string.Empty, e.Message);
            return View(viewModel);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTotalPrice(int categoryId, double distance, double duration)
    {
        var categoryRate = (await _categoryService.GetCategoryByIdAsync(categoryId)).Rate;

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
            var categories = (await _taxiService.GetAvailableTaxiTypesAsync()).ToList();

            int currentCategoryId = reservation.Taxi.CategoryId;
            string currentCategoryName = reservation.Taxi.Category.Name;

            var categoriesWithCurrent = EnsureCurrentCategoryIncluded(categories, currentCategoryId, currentCategoryName);
            
            var model = _mapper.Map<ReservationEditViewModel>(reservation);

            model.Categories = categoriesWithCurrent;
            model.CategoryId = currentCategoryId;

            return View(model);
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }

    private static List<CategoryPairViewModel> EnsureCurrentCategoryIncluded(
        List<CategoryPairViewModel> categories, int currentCategoryId, string currentCategoryName)
    {
        if (categories.TrueForAll(c => c.Id != currentCategoryId))
        {
            categories.Insert(0, new CategoryPairViewModel
            {
                Id = currentCategoryId,
                Name = currentCategoryName
            });
        }
        return categories;
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(ReservationEditViewModel viewModel)
    {
        if (viewModel.RideStatus != RideStatus.InProgress)
        {
            ModelState.AddModelError(string.Empty, "Reservation can only be edited when the ride is in progress.");
            return View(viewModel);
        }
        
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }
        
        try
        {
            await _reservationService.UpdateReservationAsync(viewModel);
            return RedirectToAction(nameof(MyReservations));
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