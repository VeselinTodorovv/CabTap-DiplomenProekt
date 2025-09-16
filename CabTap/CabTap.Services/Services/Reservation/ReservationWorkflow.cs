using CabTap.Contracts.Services.Identity;
using CabTap.Contracts.Services.Utilities;
using CabTap.Core.Entities;
using CabTap.Core.Entities.Enums;
using CabTap.Shared.Reservation;
using CabTap.Shared.Taxi;

namespace CabTap.Services.Services.Reservation;

using Reservation=Core.Entities.Reservation;

public class ReservationWorkflow
{
    private readonly ITaxiManagerService _taxiManagerService;
    private readonly IDateTimeService _dateTimeService;
    
    public ReservationWorkflow(ITaxiManagerService taxiManagerService, IDateTimeService dateTimeService)
    {
        _taxiManagerService = taxiManagerService;
        _dateTimeService = dateTimeService;
    }

    public async Task<TaxiAllViewModel> AssignTaxiAsync(int categoryId, int requestedPassengers)
    {
        var taxi = await _taxiManagerService.FindAvailableTaxiAsync(categoryId);
        if (taxi.PassengerSeats < requestedPassengers)
        {
            taxi.PassengerSeats = requestedPassengers;
        }
        
        await _taxiManagerService.UpdateTaxiStatusAsync(taxi.Id, TaxiStatus.Busy);
        return taxi;
    }

    public void SetReservationDetails(Reservation reservation, ApplicationUser user, TaxiAllViewModel taxi)
    {
        reservation.UserId = user.Id;
        reservation.TaxiId = taxi.Id;

        if (reservation.ReservationType != ReservationType.OnDemand)
        {
            reservation.ReservationDateTime = reservation.ReservationDateTime.ToUniversalTime();
            return;
        }

        var currentDateTime = _dateTimeService.GetCurrentDateTime();
        reservation.ReservationDateTime = currentDateTime;
    }
    
    public async Task UpdateReservationStatusAsync(Reservation reservation, RideStatus newStatus, IUserService userService, IAuditService audit)
    {
        if (reservation.RideStatus != RideStatus.InProgress)
        {
            return;
        }

        reservation.RideStatus = newStatus;
        
        var currentUser = await userService.GetCurrentUserAsync();
        var userName = currentUser.UserName;
        
        audit.SetModificationAuditInfo(reservation, userName);
        await _taxiManagerService.UpdateTaxiStatusAsync(reservation.TaxiId, TaxiStatus.Available);
    }
    
    public async Task<int?> ChangeTaxiIfCategoryChangedAsync(ReservationEditViewModel vm, Reservation existing)
    {
        if (existing.Taxi.CategoryId == vm.CategoryId)
        {
            return null;
        }

        var newTaxi = await AssignTaxiAsync(vm.CategoryId, vm.PassengersCount);
        await _taxiManagerService.UpdateTaxiStatusAsync(existing.TaxiId, TaxiStatus.Available);
        
        return newTaxi.Id;
    }
}