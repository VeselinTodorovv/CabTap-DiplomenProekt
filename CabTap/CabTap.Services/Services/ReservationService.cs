using AutoMapper;
using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Core.Entities;
using CabTap.Core.Entities.Enums;
using CabTap.Shared.Reservation;

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

    public async Task<ReservationDetailsViewModel> GetReservationByIdAsync(int reservationId)
    {
        var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);

        var model = _mapper.Map<ReservationDetailsViewModel>(reservation);

        return model;
    }

    public async Task AddReservationAsync(ReservationCreateViewModel reservationViewModel)
    {
        var user = await _userService.GetCurrentUserAsync();

        if (user != null)
        {
            var reservation = _mapper.Map<Reservation>(reservationViewModel);
            var taxi = (await _taxiService.GetAllTaxisAsync())
                .FirstOrDefault(x => x.TaxiStatus == TaxiStatus.Available && x.CategoryId == reservationViewModel.CategoryId) ??
                        throw new InvalidOperationException("There are no available taxis fitting your criteria");

            reservation.UserId = user.Id;
            reservation.TaxiId = taxi.Id;
            
            // Get duration from leaflet

            // Make up some dummy price calculation

            // Set selected taxi status as busy, so it can't be assigned to other reservations
            await _taxiService.UpdateTaxiTypeAsync(taxi.Id, TaxiStatus.Busy);

            reservation.CreatedBy = user.UserName;
            reservation.CreatedOn = DateTime.Now;
            reservation.LastModifiedBy = user.UserName;
            reservation.LastModifiedOn = DateTime.Now;

            await _reservationRepository.AddReservationAsync(reservation);
        }
    }

    public async Task UpdateReservationAsync(ReservationEditViewModel reservationViewModel)
    {
        var user = await _userService.GetCurrentUserAsync();

        if (user != null)
        {
            var reservation = _mapper.Map<Reservation>(reservationViewModel);
            
            reservation.LastModifiedBy = user.UserName;
            reservation.LastModifiedOn = DateTime.Now;
        
            await _reservationRepository.UpdateReservationAsync(reservation);
        }
    }

    public async Task DeleteReservationAsync(int reservationId)
    {
        await _reservationRepository.DeleteReservationAsync(reservationId);
    }
}