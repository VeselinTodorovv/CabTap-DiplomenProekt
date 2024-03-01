using CabTap.Core.Entities.Enums;
using CabTap.Shared.Category;
using CabTap.Shared.Taxi;

namespace CabTap.Contracts.Services;

public interface ITaxiService
{
    Task<IEnumerable<TaxiAllViewModel>> GetAllTaxisAsync();
    Task<IEnumerable<TaxiAllViewModel>> GetAvailableTaxisAsync(int categoryId);
    Task<IEnumerable<CategoryPairViewModel>> GetAvailableTaxiTypes();
    Task<TaxiDetailsViewModel> GetTaxiByIdAsync(int taxiId);
    Task AddTaxiAsync(TaxiCreateViewModel taxiViewModel);
    Task UpdateTaxiAsync(TaxiEditViewModel taxiViewModel);
    Task UpdateTaxiStatusAsync(int taxiId, TaxiStatus newStatus);
    Task DeleteTaxiAsync(int taxiId);
}