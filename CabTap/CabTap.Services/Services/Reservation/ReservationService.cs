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
    private readonly IReservationRepository _repo;
    private readonly IUserService _userService;
    private readonly IAuditService _audit;
    private readonly ReservationWorkflow _workflow;
    private readonly IMapper _mapper;

    public ReservationService(IReservationRepository repo, IUserService userService, IMapper mapper, IAuditService audit, ReservationWorkflow workflow)
    {
        _repo = repo;
        _userService = userService;
        _mapper = mapper;
        _audit = audit;
        _workflow = workflow;
    }
    
    public async Task<IEnumerable<ReservationAllViewModel>> GetPaginatedReservationsAsync(string searchInput, string sortOption, string reservationType, int page, int pageSize)
    {
        var userId = await _userService.GetUserId(searchInput);
        var query = _repo.GetReservationsQuery(userId, searchInput);

        query = ReservationQueries.ApplySorting(query, sortOption);
        query = ReservationQueries.ApplyFiltering(query, reservationType);

        var reservations = await query.PaginateAsync(page, pageSize);
        
        return _mapper.Map<IEnumerable<ReservationAllViewModel>>(reservations);
    }

    public async Task<IEnumerable<ReservationAllViewModel>> GetPaginatedReservationsByUserNameAsync(string searchInput, string sortOption, string reservationType, int page, int pageSize)
    {
        var user = await _userService.GetCurrentUserAsync();
        
        var query = _repo.GetReservationsQuery(user.Id, searchInput);

        query = ReservationQueries.ApplySorting(query, sortOption);
        query = ReservationQueries.ApplyFiltering(query, reservationType);

        var reservations = await query.PaginateAsync(page, pageSize);

        return _mapper.Map<IEnumerable<ReservationAllViewModel>>(reservations);
    }

    public async Task<ReservationDetailsViewModel> GetReservationByIdAsync(string reservationId)
    {
        var reservation = await _repo.GetReservationByIdAsync(reservationId);

        reservation.ReservationDateTime = reservation.ReservationDateTime.ToLocalTime();
        var model = _mapper.Map<ReservationDetailsViewModel>(reservation);

        return model;
    }

    public async Task AddReservationAsync(ReservationCreateViewModel vm)
    {
        var user = await _userService.GetCurrentUserAsync();
        var taxi = await _workflow.AssignTaxiAsync(vm.CategoryId, vm.PassengersCount);

        var reservation = _mapper.Map<Reservation>(vm);
    
        _workflow.SetReservationDetails(reservation, user, taxi);
        _audit.SetCreationAuditInfo(reservation, user.UserName);

        await _repo.AddReservationAsync(reservation);
    }

    public async Task UpdateReservationAsync(ReservationEditViewModel vm)
    {
        var user = await _userService.GetCurrentUserAsync();
        var existing = await _repo.GetReservationByIdAsync(vm.Id);

        var newTaxiId = await _workflow.ChangeTaxiIfCategoryChangedAsync(vm, existing);
        _mapper.Map(vm, existing);
        if (newTaxiId.HasValue) existing.TaxiId = newTaxiId.Value;

        _audit.SetModificationAuditInfo(existing, user.UserName);
        await _repo.UpdateReservationAsync(existing);
    }

    public async Task DeleteReservationAsync(string reservationId)
    {
        var existing = await _repo.GetReservationByIdAsync(reservationId);
        await _workflow.UpdateReservationStatusAsync(existing, RideStatus.Canceled, _userService, _audit);
        await _repo.DeleteReservationAsync(reservationId);
    }

    public async Task MarkAsCompleted(string reservationId)
    {
        var reservation = await _repo.GetReservationByIdAsync(reservationId);
        await _workflow.UpdateReservationStatusAsync(reservation, RideStatus.Finished, _userService, _audit);
    }

    public async Task MarkAsCanceled(string reservationId)
    {
        var reservation = await _repo.GetReservationByIdAsync(reservationId);
        await _workflow.UpdateReservationStatusAsync(reservation, RideStatus.Canceled, _userService, _audit);
    }
}