using CabTap.Core.Entities.Enums;
using CabTap.Shared.Category;
using CabTap.Shared.Reservation;
using CabTap.Shared.Taxi;

namespace CabTap.Contracts.Services.Utilities;

using Reservation=Core.Entities.Reservation;

public interface ITaxiManagerService
{
    Task<TaxiAllViewModel> FindAvailableTaxiAsync(int categoryId);
    Task<IEnumerable<CategoryPairViewModel>> GetAvailableTaxiTypesAsync();
    Task UpdateTaxiStatusAsync(int taxiId, TaxiStatus newStatus);
    Task<int?> GetNewTaxiIdIfCategoryChangedAsync(ReservationEditViewModel reservationViewModel, Reservation existingReservation);
}