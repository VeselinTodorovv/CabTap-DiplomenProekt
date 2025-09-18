using AutoMapper;
using CabTap.Contracts.Repositories.Reservation;
using CabTap.Contracts.Services.Identity;
using CabTap.Contracts.Services.Reservation;
using CabTap.Contracts.Services.Utilities;
using CabTap.Core.Entities.Enums;
using CabTap.Services.Infrastructure;
using CabTap.Shared.Reservation;

namespace CabTap.Services.Services.Reservation;

using Reservation=Core.Entities.Reservation;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUserService _userService;
    private readonly IAuditService _auditService;
    private readonly ReservationWorkflow _reservationWorkflow;
    private readonly IMapper _mapper;

    public ReservationService(IReservationRepository reservationRepository, IUserService userService, IMapper mapper, IAuditService auditService, ReservationWorkflow reservationWorkflow)
    {
        _reservationRepository = reservationRepository;
        _userService = userService;
        _mapper = mapper;
        _auditService = auditService;
        _reservationWorkflow = reservationWorkflow;
    }
    
    public async Task<IEnumerable<ReservationAllViewModel>> GetPaginatedReservationsAsync(string searchInput, string sortOption, string reservationType, int page, int pageSize)
    {
        var userId = await _userService.GetUserId(searchInput);
        var query = _reservationRepository.GetReservationsQuery(userId, searchInput);

        query = ReservationQueries.ApplySorting(query, sortOption);
        query = ReservationQueries.ApplyFiltering(query, reservationType);

        var reservations = await query.PaginateAsync(page, pageSize);
        
        return _mapper.Map<IEnumerable<ReservationAllViewModel>>(reservations);
    }

    public async Task<IEnumerable<ReservationAllViewModel>> GetPaginatedReservationsByUserNameAsync(string searchInput, string sortOption, string reservationType, int page, int pageSize)
    {
        var user = await _userService.GetCurrentUserAsync();
        var query = _reservationRepository.GetReservationsQuery(user.Id, searchInput);

        query = ReservationQueries.ApplySorting(query, sortOption);
        query = ReservationQueries.ApplyFiltering(query, reservationType);

        var reservations = await query.PaginateAsync(page, pageSize);

        return _mapper.Map<IEnumerable<ReservationAllViewModel>>(reservations);
    }

    public async Task<ReservationDetailsViewModel> GetReservationByIdAsync(string reservationId)
    {
        var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);

        var model = _mapper.Map<ReservationDetailsViewModel>(reservation);

        return model;
    }

    public async Task AddReservationAsync(ReservationCreateViewModel reservationViewModel)
    {
        var user = await _userService.GetCurrentUserAsync();
        var taxi = await _reservationWorkflow.AssignTaxiAsync(reservationViewModel.CategoryId, reservationViewModel.PassengersCount);

        var reservation = _mapper.Map<Reservation>(reservationViewModel);
    
        _reservationWorkflow.SetReservationDetails(reservation, user, taxi);
        _auditService.SetCreationAuditInfo(reservation, user.UserName);

        await _reservationRepository.AddReservationAsync(reservation);
    }

    public async Task UpdateReservationAsync(ReservationEditViewModel reservationViewModel)
    {
        //TODO: Research DB Transactions & Unit Of Work.
        //Current implementation updates the taxi stats first, then tries to create a reservation. If creating a reservation fails, the taxi remains busy for no reason.
        //Transactions provide a way to have either all succeed or fail, no in between.
        
        var user = await _userService.GetCurrentUserAsync();
        var existing = await _reservationRepository.GetReservationByIdAsync(reservationViewModel.Id);

        var newTaxiId = await _reservationWorkflow.ChangeTaxiIfCategoryChangedAsync(reservationViewModel, existing);
        _mapper.Map(reservationViewModel, existing);

        if (newTaxiId.HasValue)
        {
            existing.TaxiId = newTaxiId.Value;
        }

        _auditService.SetModificationAuditInfo(existing, user.UserName);
        await _reservationRepository.UpdateReservationAsync(existing);
    }

    public async Task DeleteReservationAsync(string reservationId)
    {
        var existing = await _reservationRepository.GetReservationByIdAsync(reservationId);
        
        await _reservationWorkflow.UpdateReservationStatusAsync(existing, RideStatus.Canceled, _userService, _auditService);
        await _reservationRepository.DeleteReservationAsync(reservationId);
    }

    public async Task MarkAsCompleted(string reservationId)
    {
        var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);
        await _reservationWorkflow.UpdateReservationStatusAsync(reservation, RideStatus.Finished, _userService, _auditService);
    }

    public async Task MarkAsCanceled(string reservationId)
    {
        var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);
        await _reservationWorkflow.UpdateReservationStatusAsync(reservation, RideStatus.Canceled, _userService, _auditService);
    }
}