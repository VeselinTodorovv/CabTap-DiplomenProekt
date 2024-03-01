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
    private readonly IMapper _mapper;
    
    public TaxiService(ITaxiRepository taxiRepository, IUserService userService, IMapper mapper)
    {
        _taxiRepository = taxiRepository;
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TaxiAllViewModel>> GetAllTaxisAsync()
    {
        var taxis = await _taxiRepository.GetAllTaxisAsync();

        var taxiViewModels = _mapper.Map<IEnumerable<TaxiAllViewModel>>(taxis);
        
        return taxiViewModels;
    }

    public async Task<IEnumerable<TaxiAllViewModel>> GetAvailableTaxisAsync(int categoryId)
    {
        var taxis = (await _taxiRepository.GetAllTaxisAsync()).Where(x =>
            x.TaxiStatus == TaxiStatus.Available && x.CategoryId == categoryId);

        var model = _mapper.Map<IEnumerable<TaxiAllViewModel>>(taxis);

        return model;
    }

    public async Task<IEnumerable<CategoryPairViewModel>> GetAvailableTaxiTypes()
    {
        var taxis = (await _taxiRepository.GetAllTaxisAsync()).ToList();

        var availableCategories = taxis
            .Where(x => x is { TaxiStatus: TaxiStatus.Available })
            .Select(x => new CategoryPairViewModel
            {
                Id = x.Category.Id,
                Name = x.Category.Name
            })
            .Distinct();

        return availableCategories;
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

        if (user != null)
        {
            var taxi = _mapper.Map<Taxi>(taxiViewModel);

            taxi.CreatedBy = user.UserName;
            taxi.CreatedOn = DateTime.Now;
            taxi.LastModifiedBy = user.UserName;
            taxi.LastModifiedOn = DateTime.Now;
        
            await _taxiRepository.AddTaxiAsync(taxi);
        }
    }

    public async Task UpdateTaxiAsync(TaxiEditViewModel taxiViewModel)
    {
        var user = await _userService.GetCurrentUserAsync();

        if (user != null)
        {
            var taxi = _mapper.Map<Taxi>(taxiViewModel);

            taxi.LastModifiedBy = user.UserName;
            taxi.LastModifiedOn = DateTime.Now;
        
            await _taxiRepository.UpdateTaxiAsync(taxi);
        }
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