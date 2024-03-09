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

    public async Task<ReservationDetailsViewModel> GetReservationByIdAsync(string reservationId)
    {
        var reservation = await _reservationRepository.GetReservationsByIdAsync(reservationId);

        var model = _mapper.Map<ReservationDetailsViewModel>(reservation);

        return model;
    }

    public async Task<IEnumerable<ReservationAllViewModel>> GetReservationsByUserIdAsync()
    {
        var user = await _userService.GetCurrentUserAsync();
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not logged in.");
        }
        
        var reservations = await _reservationRepository.GetReservationsByUserIdAsync(user.Id);
        
        var reservationViewModel = _mapper.Map<IEnumerable<ReservationAllViewModel>>(reservations);
        
        return reservationViewModel;
    }

    public async Task AddReservationAsync(ReservationCreateViewModel reservationViewModel)
    {
        var user = await _userService.GetCurrentUserAsync();
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not logged in.");
        }

        var taxi = await _taxiService.FindAvailableTaxiAsync(reservationViewModel.CategoryId);

        var reservation = _mapper.Map<Reservation>(reservationViewModel);

        reservation.UserId = user.Id;
        reservation.TaxiId = taxi.Id;

        reservation.UpdateAuditInfo(user.UserName);

        await _taxiService.UpdateTaxiStatusAsync(taxi.Id, TaxiStatus.Busy);

        await _reservationRepository.AddReservationAsync(reservation);
    }

    public async Task UpdateReservationAsync(ReservationEditViewModel reservationViewModel)
    {
        var user = await _userService.GetCurrentUserAsync();
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not logged in");
        }

        var newTaxiId = 0;

        var existingReservation = await _reservationRepository.GetReservationsByIdAsync(reservationViewModel.Id);
        if (existingReservation.Taxi.CategoryId != reservationViewModel.CategoryId)
        {
            var taxi = await _taxiService.FindAvailableTaxiAsync(reservationViewModel.CategoryId);
            
            var oldId = existingReservation.TaxiId;
            newTaxiId = taxi.Id;
            
            await _taxiService.UpdateTaxiStatusAsync(oldId, TaxiStatus.Available);
            await _taxiService.UpdateTaxiStatusAsync(newTaxiId, TaxiStatus.Busy);
        }

        _mapper.Map(reservationViewModel, existingReservation);
        if (newTaxiId != 0)
        {
            existingReservation.TaxiId = newTaxiId;
        }

        existingReservation.UpdateAuditInfo(user.UserName);
        
        await _reservationRepository.UpdateReservationAsync(existingReservation);
    }

    public async Task DeleteReservationAsync(string id)
    {
        var reservation = await _reservationRepository.GetReservationsByIdAsync(id);
        
        await _taxiService.UpdateTaxiStatusAsync(reservation.TaxiId, TaxiStatus.Available);
        
        await _reservationRepository.DeleteReservationAsync(id);
    }

    public async Task<IEnumerable<ReservationAllViewModel>> GetPaginatedReservationsAsync(int page, int pageSize)
    {
        var reservations = await _reservationRepository.GetPaginatedReservationsAsync(page, pageSize);
        var reservationViewModels = _mapper.Map<IEnumerable<ReservationAllViewModel>>(reservations);
        return reservationViewModels;
    }

    public async Task<IEnumerable<ReservationAllViewModel>> GetPaginatedReservationsByUserIdAsync(int page, int pageSize)
    {
        var user = await _userService.GetCurrentUserAsync();
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not logged in.");
        }

        var reservations = await _reservationRepository.GetPaginatedReservationsByUserIdAsync(user.Id, page, pageSize);
        var reservationViewModels = _mapper.Map<IEnumerable<ReservationAllViewModel>>(reservations);
        return reservationViewModels;
    }
}