using AutoMapper;
using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Core.Entities;
using CabTap.Shared.Reservation;

namespace CabTap.Services.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public ReservationService(IReservationRepository reservationRepository, IUserService userService, IMapper mapper)
    {
        _reservationRepository = reservationRepository;
        _userService = userService;
        _mapper = mapper;
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
        var user = await _userService.GetCurrentUserName();

        if (user != null)
        {
            var reservation = _mapper.Map<Reservation>(reservationViewModel);

            reservation.CreatedBy = user;
            reservation.CreatedOn = DateTime.Now;
            reservation.LastModifiedBy = user;
            reservation.LastModifiedOn = DateTime.Now;

            await _reservationRepository.AddReservationAsync(reservation);
        }
    }

    public async Task UpdateReservationAsync(ReservationEditViewModel reservationViewModel)
    {
        var user = await _userService.GetCurrentUserName();

        if (user != null)
        {
            var reservation = _mapper.Map<Reservation>(reservationViewModel);
            
            reservation.LastModifiedBy = user;
            reservation.LastModifiedOn = DateTime.Now;
        
            await _reservationRepository.UpdateReservationAsync(reservation);
        }
    }

    public async Task DeleteReservationAsync(int reservationId)
    {
        await _reservationRepository.DeleteReservationAsync(reservationId);
    }
}