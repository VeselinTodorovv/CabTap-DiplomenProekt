using AutoMapper;
using CabTap.Contracts.Repositories.Taxi;
using CabTap.Contracts.Services.Identity;
using CabTap.Contracts.Services.Taxi;
using CabTap.Contracts.Services.Utilities;
using CabTap.Core.Entities.Enums;
using CabTap.Services.Infrastructure;
using CabTap.Shared.Category;
using CabTap.Shared.Taxi;

namespace CabTap.Services.Services.Taxi;

public class TaxiService : ITaxiService
{
    private readonly ITaxiRepository _taxiRepository;
    private readonly IUserService _userService;
    private readonly ICategoryService _categoryService;
    private readonly IDateTimeService _dateTimeService;
    private readonly IMapper _mapper;
    private readonly IAuditService _auditService;
    
    public TaxiService(ITaxiRepository taxiRepository, IUserService userService, IMapper mapper, ICategoryService categoryService, IDateTimeService dateTimeService, IAuditService auditService)
    {
        _taxiRepository = taxiRepository;
        _userService = userService;
        _dateTimeService = dateTimeService;
        _auditService = auditService;
        _mapper = mapper;
        _categoryService = categoryService;
    }

    public async Task<IEnumerable<TaxiAllViewModel>> GetPaginatedTaxisAsync(int page, int pageSize)
    {
        var query = _taxiRepository.GetTaxisQuery();
        var taxis = await query.PaginateAsync(page, pageSize);

        var reservationViewModels = _mapper.Map<IEnumerable<TaxiAllViewModel>>(taxis);
        return reservationViewModels;
    }

    public async Task<IEnumerable<CategoryPairViewModel>> GetAvailableTaxiTypesAsync()
    {
        var taxis = _taxiRepository.GetTaxisQuery()
            .Where(x => x.TaxiStatus == TaxiStatus.Available);
        var allCategories = await _categoryService.GetAllCategoriesAsync();

        var availableCategoryIds = taxis.Select(x => x.CategoryId).Distinct();
        var availableCategories = allCategories.Where(category => availableCategoryIds.Contains(category.Id));

        var categoryViewModels = availableCategories.Select(x => new CategoryPairViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Rate = x.Rate
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

        var taxi = _mapper.Map<Core.Entities.Taxi>(taxiViewModel);

        var dateTime = _dateTimeService.GetCurrentDateTime();
        _auditService.UpdateAuditInfo(taxi, dateTime, user.UserName);

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
        _auditService.UpdateAuditInfo(existingTaxi, dateTime, user.UserName);

        await _taxiRepository.UpdateTaxiAsync(existingTaxi);
    }

    public async Task DeleteTaxiAsync(int taxiId)
    {
        await _taxiRepository.DeleteTaxiAsync(taxiId);
    }
}