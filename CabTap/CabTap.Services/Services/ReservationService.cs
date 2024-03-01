using AutoMapper;
using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Core.Entities;
using CabTap.Core.Entities.Enums;
using CabTap.Shared.Reservation;
using CabTap.Shared.Taxi;

namespace CabTap.Services.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUserService _userService;
    private readonly ITaxiService _taxiService;
    private readonly IMapper _mapper;

    public ReservationService(IReservationRepository reservationRepository, IUserService userService, IMapper mapper, ITaxiService taxiService)
    {
        _reservationRepository = reservationRepository;
        _userService = userService;
        _mapper = mapper;
        _taxiService = taxiService;
    }

    public async Task<IEnumerable<ReservationAllViewModel>> GetAllReservationsAsync()
    {
        var reservations = await _reservationRepository.GetAllReservationsAsync();

        var reservationViewModels = _mapper.Map<IEnumerable<ReservationAllViewModel>>(reservations);

        return reservationViewModels;
    }

    public async Task<ReservationDetailsViewModel> GetReservationByIdAsync(string reservationId)
    {
        var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);

        var model = _mapper.Map<ReservationDetailsViewModel>(reservation);

        return model;
    }

    public async Task AddReservationAsync(ReservationCreateViewModel reservationViewModel)
    {
        var user = await _userService.GetCurrentUserAsync() ??
                   throw new UnauthorizedAccessException("User is not logged in.");

        var taxi = await FindAvailableTaxis(reservationViewModel.CategoryId);

        var reservation = _mapper.Map<Reservation>(reservationViewModel);

        reservation.UserId = user.Id;
        reservation.TaxiId = taxi.Id;

        reservation.CreatedBy = user.UserName;
        reservation.CreatedOn = DateTime.Now;
        reservation.LastModifiedBy = user.UserName;
        reservation.LastModifiedOn = DateTime.Now;

        await _taxiService.UpdateTaxiStatusAsync(taxi.Id, TaxiStatus.Busy);

        await _reservationRepository.AddReservationAsync(reservation);
    }

    public async Task UpdateReservationAsync(ReservationEditViewModel reservationViewModel)
    {
        var user = await _userService.GetCurrentUserAsync() ??
                   throw new UnauthorizedAccessException("User is not logged in.");

        var taxi = await FindAvailableTaxis(reservationViewModel.CategoryId);
        var existingReservation = await _reservationRepository.GetReservationByIdAsync(reservationViewModel.Id);

        var oldTaxiId = existingReservation.TaxiId;
        var newTaxiId = taxi.Id;

        if (oldTaxiId != newTaxiId)
        {
            await _taxiService.UpdateTaxiStatusAsync(oldTaxiId, TaxiStatus.Available);
            await _taxiService.UpdateTaxiStatusAsync(newTaxiId, TaxiStatus.Busy);
        }

        var reservation = _mapper.Map<Reservation>(reservationViewModel);
        reservation.TaxiId = newTaxiId;
        reservation.LastModifiedBy = user.UserName;
        reservation.LastModifiedOn = DateTime.Now;

        await _reservationRepository.UpdateReservationAsync(reservation);
    }

    public async Task DeleteReservationAsync(string id)
    {
        var reservation = await _reservationRepository.GetReservationByIdAsync(id);
        
        await _taxiService.UpdateTaxiStatusAsync(reservation.TaxiId, TaxiStatus.Available);
        
        await _reservationRepository.DeleteReservationAsync(id);
    }

    private async Task<TaxiAllViewModel> FindAvailableTaxis(int categoryId)
    {
        var taxis = await _taxiService.GetAvailableTaxisAsync(categoryId);
        
        return taxis.FirstOrDefault() ?? 
               throw new InvalidOperationException("No available taxis found.");
    }
}