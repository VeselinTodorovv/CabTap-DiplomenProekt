using AutoMapper;
using CabTap.Contracts.Repositories.Taxi;
using CabTap.Contracts.Services.Utilities;
using CabTap.Core.Entities.Enums;
using CabTap.Shared.Category;
using CabTap.Shared.Taxi;
using Microsoft.EntityFrameworkCore;

namespace CabTap.Services.Services.Utilities;

public class TaxiManagerService : ITaxiManagerService
{
    private readonly ITaxiRepository _taxiRepository;
    private readonly IMapper _mapper;

    public TaxiManagerService(ITaxiRepository taxiRepository, IMapper mapper)
    {
        _taxiRepository = taxiRepository;
        _mapper = mapper;
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

        if (taxi.TaxiStatus == newStatus)
        {
            return;
        }
        
        taxi.TaxiStatus = newStatus;
        await _taxiRepository.UpdateTaxiAsync(taxi);
    }

    public async Task<IEnumerable<CategoryPairViewModel>> GetAvailableTaxiTypesAsync()
    {
        var availableCategories = await _taxiRepository.GetTaxisQuery()
            .Where(taxi => taxi.TaxiStatus == TaxiStatus.Available)
            .Include(taxi => taxi.Category)
            .Select(taxi => taxi.Category)
            .Distinct()
            .ToListAsync();

        return availableCategories.Select(category => new CategoryPairViewModel
        {
            Id = category.Id,
            Name = category.Name,
            Rate = category.Rate
        });
    }
}