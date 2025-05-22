using AutoMapper;
using CabTap.Contracts.Repositories.Reservation;
using CabTap.Contracts.Services.Identity;
using CabTap.Contracts.Services.Reservation;
using CabTap.Contracts.Services.Utilities;
using CabTap.Core.Entities.Enums;
using CabTap.Services.Infrastructure;
using CabTap.Shared.Reservation;

namespace CabTap.Services.Services.Reservation;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IUserService _userService;
    private readonly IDateTimeService _dateTimeService;
    private readonly IAuditService _auditService;
    private readonly ITaxiManagerService _taxiManagerService;
    private readonly IMapper _mapper;

    public ReservationService(IReservationRepository reservationRepository, IUserService userService, IMapper mapper, IDateTimeService dateTimeService, IAuditService auditService, ITaxiManagerService taxiManagerService)
    {
        _reservationRepository = reservationRepository;
        _userService = userService;
        _mapper = mapper;
        _dateTimeService = dateTimeService;
        _auditService = auditService;
        _taxiManagerService = taxiManagerService;
    }
    
    public async Task<IEnumerable<ReservationAllViewModel>> GetPaginatedReservationsAsync(string searchInput, string sortOption, string reservationType, int page, int pageSize)
    {
        var userId = await _userService.GetUserId(searchInput);
        var query = _reservationRepository.GetReservationsQuery(userId, searchInput);

        query = ApplySorting(query, sortOption);
        query = ApplyFiltering(query, reservationType);

        var reservations = await query.PaginateAsync(page, pageSize);
        
        return _mapper.Map<IEnumerable<ReservationAllViewModel>>(reservations);
    }

    public async Task<IEnumerable<ReservationAllViewModel>> GetPaginatedReservationsByUserNameAsync(string searchInput, string sortOption, string reservationType, int page, int pageSize)
    {
        var user = await _userService.GetCurrentUserAsync();
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not logged in");
        }
        
        var query = _reservationRepository.GetReservationsQuery(user.Id, searchInput);

        query = ApplySorting(query, sortOption);
        query = ApplyFiltering(query, reservationType);

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
        if (user == null)
        {
            throw new UnauthorizedAccessException("User is not logged in.");
        }

        var taxi = await _taxiManagerService.FindAvailableTaxiAsync(reservationViewModel.CategoryId);
        if (taxi.PassengerSeats < reservationViewModel.PassengersCount)
        {
            reservationViewModel.PassengersCount = taxi.PassengerSeats;
        }

        var reservation = _mapper.Map<Core.Entities.Reservation>(reservationViewModel);
        
        var dateTime = _dateTimeService.GetCurrentDateTime();

        reservation.UserId = user.Id;
        reservation.TaxiId = taxi.Id;

        if (reservation.ReservationType == ReservationType.OnDemand)
        {
            reservation.ReservationDateTime = dateTime;
        }

        _auditService.UpdateAuditInfo(reservation, dateTime, user.UserName);

        await _taxiManagerService.UpdateTaxiStatusAsync(taxi.Id, TaxiStatus.Busy);

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

        var existingReservation = await _reservationRepository.GetReservationByIdAsync(reservationViewModel.Id);
        if (existingReservation.Taxi.CategoryId != reservationViewModel.CategoryId)
        {
            var taxi = await _taxiManagerService.FindAvailableTaxiAsync(reservationViewModel.CategoryId);
            
            var oldId = existingReservation.TaxiId;
            newTaxiId = taxi.Id;
            
            await _taxiManagerService.UpdateTaxiStatusAsync(oldId, TaxiStatus.Available);
            await _taxiManagerService.UpdateTaxiStatusAsync(newTaxiId, TaxiStatus.Busy);
        }

        _mapper.Map(reservationViewModel, existingReservation);
        if (newTaxiId != 0)
        {
            existingReservation.TaxiId = newTaxiId;
        }

        var dateTime = _dateTimeService.GetCurrentDateTime();
        _auditService.UpdateAuditInfo(existingReservation, dateTime, user.UserName);
        
        await _reservationRepository.UpdateReservationAsync(existingReservation);
    }

    public async Task DeleteReservationAsync(string reservationId)
    {
        var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);
        
        await _taxiManagerService.UpdateTaxiStatusAsync(reservation.TaxiId, TaxiStatus.Available);
        
        await _reservationRepository.DeleteReservationAsync(reservationId);
    }

    private static IQueryable<Core.Entities.Reservation> ApplySorting(IQueryable<Core.Entities.Reservation> query, string sortOption)
    {
        return sortOption switch
        {
            "priceAsc" => query.OrderBy(x => x.Price),
            "priceDesc" => query.OrderByDescending(x => x.Price),
            "distanceAsc" => query.OrderBy(x => x.Distance),
            "distanceDesc" => query.OrderByDescending(x => x.Distance),
            "dateAsc" => query.OrderBy(x => x.ReservationDateTime),
            "dateDesc" => query.OrderByDescending(x => x.ReservationDateTime),
            "oldest" => query.OrderBy(r => r.CreatedOn),
            _ => query.OrderByDescending(r => r.LastModifiedOn)
        };
    }

    private static IQueryable<Core.Entities.Reservation> ApplyFiltering(IQueryable<Core.Entities.Reservation> query, string reservationType)
    {
        return !string.IsNullOrEmpty(reservationType)
            ? query.Where(r => r.ReservationType == Enum.Parse<ReservationType>(reservationType))
            : query;
    }

    public async Task MarkAsCompleted(string reservationId)
    {
        var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);
        if (reservation.RideStatus == RideStatus.InProgress)
        {
            reservation.RideStatus = RideStatus.Finished;
            
            var dateTime = _dateTimeService.GetCurrentDateTime();
            var userName = (await _userService.GetCurrentUserAsync())!.UserName;
            _auditService.UpdateAuditInfo(reservation, dateTime, userName);
            
            await _reservationRepository.UpdateReservationAsync(reservation);
            await _taxiManagerService.UpdateTaxiStatusAsync(reservation.TaxiId, TaxiStatus.Available);
        }
    }

    public async Task MarkAsCanceled(string reservationId)
    {
        var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);
        if (reservation.RideStatus == RideStatus.InProgress)
        {
            reservation.RideStatus = RideStatus.Canceled;

            var dateTime = _dateTimeService.GetCurrentDateTime();
            var userName = (await _userService.GetCurrentUserAsync())!.UserName;
            _auditService.UpdateAuditInfo(reservation, dateTime, userName);
            
            await _reservationRepository.UpdateReservationAsync(reservation);
            await _taxiManagerService.UpdateTaxiStatusAsync(reservation.TaxiId, TaxiStatus.Available);
        }
    }
}