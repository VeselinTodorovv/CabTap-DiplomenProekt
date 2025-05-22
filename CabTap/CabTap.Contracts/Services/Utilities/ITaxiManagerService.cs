using CabTap.Core.Entities.Enums;
using CabTap.Shared.Taxi;

namespace CabTap.Contracts.Services.Utilities;

public interface ITaxiManagerService
{
    Task<TaxiAllViewModel> FindAvailableTaxiAsync(int categoryId);
    Task UpdateTaxiStatusAsync(int taxiId, TaxiStatus newStatus);
}