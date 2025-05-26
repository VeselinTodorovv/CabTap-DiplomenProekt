using AutoMapper;
using CabTap.Contracts.Repositories.Taxi;
using CabTap.Contracts.Services.Taxi;
using CabTap.Contracts.Services.Utilities;
using CabTap.Core.Entities.Enums;
using CabTap.Shared.Category;
using CabTap.Shared.Reservation;
using CabTap.Shared.Taxi;
using Microsoft.EntityFrameworkCore;

namespace CabTap.Services.Services.Utilities;

public class TaxiManagerService : ITaxiManagerService
{
    private readonly ITaxiRepository _taxiRepository;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public TaxiManagerService(ITaxiRepository taxiRepository, IMapper mapper, ICategoryService categoryService)
    {
        _taxiRepository = taxiRepository;
        _mapper = mapper;
        _categoryService = categoryService;
    }

    public async Task<TaxiAllViewModel> FindAvailableTaxiAsync(int categoryId)
    {
        var taxi = await _taxiRepository.GetTaxisQuery()
            .FirstOrDefaultAsync(x => x.TaxiStatus == TaxiStatus.Available &&
                                      x.CategoryId == categoryId);

        var model = _mapper.Map<TaxiAllViewModel>(taxi);

        return model;
    }

    public async Task UpdateTaxiStatusAsync(int taxiId, TaxiStatus newStatus)
    {
        var taxi = await _taxiRepository.GetTaxiByIdAsync(taxiId);

        taxi.TaxiStatus = newStatus;

        await _taxiRepository.UpdateTaxiAsync(taxi);
    }
    
    public async Task<int?> GetNewTaxiIdIfCategoryChangedAsync(ReservationEditViewModel reservationViewModel, Core.Entities.Reservation existingReservation)
    {
        if (existingReservation.Taxi.CategoryId == reservationViewModel.CategoryId)
        {
            return null;
        }
        
        var taxi = await FindAvailableTaxiAsync(reservationViewModel.CategoryId);
        
        await UpdateTaxiStatusAsync(existingReservation.TaxiId, TaxiStatus.Available);
        await UpdateTaxiStatusAsync(taxi.Id, TaxiStatus.Busy);
        
        return taxi.Id;
    }
    
    private static IEnumerable<CategoryPairViewModel> MapToCategoryPairViewModels(IEnumerable<CategoryPairViewModel> categories)
    {
        return categories.Select(x => new CategoryPairViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Rate = x.Rate
        });
    }

    public async Task<IEnumerable<CategoryPairViewModel>> GetAvailableTaxiTypesAsync()
    {
        var availableTaxis = _taxiRepository.GetTaxisQuery()
            .Where(taxi => taxi.TaxiStatus == TaxiStatus.Available);

        var allCategories = (await _categoryService.GetAllCategoriesAsync())
            .ToList();
        
        var availableCategoryIds = availableTaxis
            .Select(taxi => taxi.CategoryId)
            .Distinct()
            .ToHashSet();

        var availableCategories = allCategories
            .Where(category => availableCategoryIds.Contains(category.Id));

        return MapToCategoryPairViewModels(availableCategories);
    }
}