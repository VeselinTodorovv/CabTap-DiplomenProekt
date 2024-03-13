using AutoMapper;
using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Core.Entities;
using CabTap.Core.Entities.Enums;
using CabTap.Shared.Category;
using CabTap.Shared.Taxi;

namespace CabTap.Services.Services;

public class TaxiService : ITaxiService
{
    private readonly ITaxiRepository _taxiRepository;
    private readonly IUserService _userService;
    private readonly ICategoryService _categoryService;
    private readonly IDateTimeService _dateTimeService;
    private readonly IMapper _mapper;
    
    public TaxiService(ITaxiRepository taxiRepository, IUserService userService, IMapper mapper, ICategoryService categoryService, IDateTimeService dateTimeService)
    {
        _taxiRepository = taxiRepository;
        _userService = userService;
        _dateTimeService = dateTimeService;
        _mapper = mapper;
        _categoryService = categoryService;
    }

    public async Task<IEnumerable<TaxiAllViewModel>> GetPaginatedTaxisAsync(int page, int pageSize)
    {
        var reservations = await _taxiRepository.GetPaginatedTaxisAsync(page, pageSize);
        var reservationViewModels = _mapper.Map<IEnumerable<TaxiAllViewModel>>(reservations);
        return reservationViewModels;
    }

    public async Task<TaxiAllViewModel> FindAvailableTaxiAsync(int categoryId)
    {
        var taxis = (await _taxiRepository.GetAllTaxisAsync())
            .FirstOrDefault(x => x.TaxiStatus == TaxiStatus.Available &&
                                 x.CategoryId == categoryId);

        var model = _mapper.Map<TaxiAllViewModel>(taxis);

        return model;
    }

    public async Task<IEnumerable<CategoryPairViewModel>> GetAvailableTaxiTypesAsync()
    {
        var taxis = await _taxiRepository.GetAllTaxisAsync();
        var allCategories = await _categoryService.GetAllCategoriesAsync();

        var availableTaxis = taxis.Where(x => x is { TaxiStatus: TaxiStatus.Available });

        var availableCategoryIds = availableTaxis.Select(x => x.CategoryId).Distinct();
        var availableCategories = allCategories.Where(category => availableCategoryIds.Contains(category.Id));

        var categoryViewModels = availableCategories.Select(x => new CategoryPairViewModel
        {
            Id = x.Id,
            Name = x.Name
        });

        return categoryViewModels;
    }

    public async Task<TaxiDetailsViewModel> GetTaxiByIdAsync(int taxiId)
    {
        var taxi = await _taxiRepository.GetTaxiByIdAsync(taxiId);

        var model = _mapper.Map<TaxiDetailsViewModel>(taxi);

        return model;
    }

    public async Task AddTaxiAsync(TaxiCreateViewModel taxiViewModel)
    {
        var user = await _userService.GetCurrentUserAsync();
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not logged in");
        }

        var taxi = _mapper.Map<Taxi>(taxiViewModel);

        var dateTime = _dateTimeService.GetCurrentDateTime();
        taxi.UpdateAuditInfo(dateTime, user.UserName);

        await _taxiRepository.AddTaxiAsync(taxi);
    }

    public async Task UpdateTaxiAsync(TaxiEditViewModel taxiViewModel)
    {
        var user = await _userService.GetCurrentUserAsync();
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not logged in");
        }

        var existingTaxi = await _taxiRepository.GetTaxiByIdAsync(taxiViewModel.Id);

        _mapper.Map(taxiViewModel, existingTaxi);

        var dateTime = _dateTimeService.GetCurrentDateTime();
        existingTaxi.UpdateAuditInfo(dateTime, user.UserName);

        await _taxiRepository.UpdateTaxiAsync(existingTaxi);
    }

    public async Task UpdateTaxiStatusAsync(int taxiId, TaxiStatus newStatus)
    {
        var taxi = await _taxiRepository.GetTaxiByIdAsync(taxiId);

        taxi.TaxiStatus = newStatus;

        await _taxiRepository.UpdateTaxiAsync(taxi);
    }

    public async Task DeleteTaxiAsync(int taxiId)
    {
        await _taxiRepository.DeleteTaxiAsync(taxiId);
    }
}