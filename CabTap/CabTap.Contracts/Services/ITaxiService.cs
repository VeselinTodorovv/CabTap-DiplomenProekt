using CabTap.Shared.Taxi;

namespace CabTap.Contracts.Services;

public interface ITaxiService
{
    Task<IEnumerable<TaxiAllViewModel>> GetAllAvailableTaxisAsync();
    Task<TaxiDetailsViewModel> GetTaxiByIdAsync(int taxiId);
    Task AddTaxiAsync(TaxiCreateViewModel taxiViewModel);
    Task UpdateTaxiAsync(TaxiEditViewModel taxiViewModel);
    Task DeleteTaxiAsync(int taxiId);
}