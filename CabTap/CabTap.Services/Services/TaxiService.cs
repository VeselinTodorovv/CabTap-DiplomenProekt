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
    private readonly ICategoryService _categoryService;
    private readonly IUserService _userService;
    
    public TaxiService(ITaxiRepository taxiRepository, ICategoryService categoryService, IUserService userService)
    {
        _taxiRepository = taxiRepository;
        _categoryService = categoryService;
        _userService = userService;
    }

    public async Task<IEnumerable<TaxiAllViewModel>> GetAllTaxisAsync()
    {
        var taxis = await _taxiRepository.GetAllTaxisAsync();

        var taxiViewModels = taxis.Select(taxi => new TaxiAllViewModel
        {
            Id = taxi!.Id,
            ManufacturerId = taxi.ManufacturerId,
            ManufacturerName = taxi.Manufacturer.Name,
            RegNumber = taxi.RegNumber,
            Description = taxi.Description,
            Picture = taxi.Picture,
            TaxiStatus = taxi.TaxiStatus,
            PassengerSeats = taxi.PassengerSeats,
            DriverId = taxi.DriverId,
            DriverName = taxi.Driver.Name,
            CategoryId = taxi.CategoryId,
            CategoryName = taxi.Category.Name,
            
            CreatedBy = taxi.CreatedBy,
            CreatedOn = taxi.CreatedOn,
            LastModifiedBy = taxi.LastModifiedBy,
            LastModifiedOn = taxi.LastModifiedOn
        })
            .Where(x => x.TaxiStatus == TaxiStatus.Available)
            .ToList();
        
        return taxiViewModels;
    }

    public async Task<IEnumerable<CategoryPairViewModel>> GetAvailableTaxiTypes()
    {
        var taxis = await _taxiRepository.GetAllTaxisAsync();
        var allCategories = await _categoryService.GetAllCategories();

        var availableTaxis = taxis.Where(x => x.TaxiStatus == TaxiStatus.Available);

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
        
        var model = new TaxiDetailsViewModel
        {
            Id = taxi.Id,
            ManufacturerId = taxi.ManufacturerId,
            Description = taxi.Description,
            Picture = taxi.Picture,
            CategoryId = taxi.CategoryId,
            CreatedBy = taxi.CreatedBy,
            CreatedOn = taxi.CreatedOn,
            DriverId = taxi.DriverId,
            PassengerSeats = taxi.PassengerSeats,
            RegNumber = taxi.RegNumber,
            TaxiStatus = taxi.TaxiStatus,
            LastModifiedBy = taxi.LastModifiedBy,
            LastModifiedOn = taxi.LastModifiedOn
        };

        return model;
    }

    public async Task AddTaxiAsync(TaxiCreateViewModel taxiViewModel)
    {
        var user = _userService.GetCurrentUserName();

        if (user != null)
        {
            var taxi = new Taxi
            {
                Id = taxiViewModel.Id,
                ManufacturerId = taxiViewModel.ManufacturerId,
                Description = taxiViewModel.Description,
                Picture = taxiViewModel.Picture,
                CategoryId = taxiViewModel.CategoryId,
                DriverId = taxiViewModel.DriverId,
                PassengerSeats = taxiViewModel.PassengerSeats,
                RegNumber = taxiViewModel.RegNumber,
                TaxiStatus = taxiViewModel.TaxiStatus,
                
                CreatedBy = user,
                CreatedOn = DateTime.Now,
                LastModifiedBy = user,
                LastModifiedOn = DateTime.Now
            };
        
            await _taxiRepository.AddTaxiAsync(taxi);
        }
    }

    public async Task UpdateTaxiAsync(TaxiEditViewModel taxiViewModel)
    {
        var user = _userService.GetCurrentUserName();

        if (user != null)
        {
            var taxi = new Taxi
            {
                Id = taxiViewModel.Id,
                ManufacturerId = taxiViewModel.ManufacturerId,
                Description = taxiViewModel.Description,
                Picture = taxiViewModel.Picture,
                CategoryId = taxiViewModel.CategoryId,
                DriverId = taxiViewModel.DriverId,
                PassengerSeats = taxiViewModel.PassengerSeats,
                RegNumber = taxiViewModel.RegNumber,
                TaxiStatus = taxiViewModel.TaxiStatus,
                CreatedBy = taxiViewModel.CreatedBy,
                CreatedOn = taxiViewModel.CreatedOn,
                
                LastModifiedBy = user,
                LastModifiedOn = DateTime.Now
            };
        
            await _taxiRepository.UpdateTaxiAsync(taxi);
        }
    }

    public async Task DeleteTaxiAsync(int taxiId)
    {
        await _taxiRepository.DeleteTaxiAsync(taxiId);
    }
}