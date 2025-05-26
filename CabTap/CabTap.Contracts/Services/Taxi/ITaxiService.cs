using CabTap.Shared.Category;
using CabTap.Shared.Taxi;

namespace CabTap.Contracts.Services.Taxi;

public interface ITaxiService
{
    Task<IEnumerable<TaxiAllViewModel>> GetPaginatedTaxisAsync(int page, int pageSize);
    Task<TaxiDetailsViewModel> GetTaxiByIdAsync(int taxiId);
    Task AddTaxiAsync(TaxiCreateViewModel taxiViewModel);
    Task UpdateTaxiAsync(TaxiEditViewModel taxiViewModel);
    Task DeleteTaxiAsync(int taxiId);
}