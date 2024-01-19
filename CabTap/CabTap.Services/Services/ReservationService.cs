using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;
using CabTap.Core.Entities;
using CabTap.Shared.Reservation;
using Microsoft.AspNetCore.Http;

namespace CabTap.Services.Services;

public class ReservationService : IReservationService
{
    private readonly IReservationRepository _reservationRepository;
    private readonly IHttpContextAccessor _contextAccessor;

    public ReservationService(IReservationRepository reservationRepository, IHttpContextAccessor contextAccessor)
    {
        _reservationRepository = reservationRepository;
        _contextAccessor = contextAccessor;
    }

    public async Task<IEnumerable<ReservationAllViewModel>> GetAllReservationsAsync()
    {
        var reservations = await _reservationRepository.GetAllReservationsAsync();

        var reservationViewModels = reservations.Select(reservation => new ReservationAllViewModel
        {
            Id = reservation!.Id,
            Destination = reservation.Destination,
            Duration = reservation.Duration,
            Price = reservation.Price,
            PassengersCount = reservation.PassengersCount,
            RideStatus = reservation.RideStatus,
            CreatedBy = reservation.CreatedBy,
            CreatedOn = reservation.CreatedOn,
            LastModifiedBy = reservation.LastModifiedBy,
            LastModifiedOn = reservation.LastModifiedOn,
            Origin = reservation.Origin,
            TaxiId = reservation.TaxiId,
            UserId = reservation.UserId
        })
            .ToList();

        return reservationViewModels;
    }

    public async Task<ReservationDetailsViewModel> GetReservationByIdAsync(int reservationId)
    {
        var reservation = await _reservationRepository.GetReservationByIdAsync(reservationId);

        var model = new ReservationDetailsViewModel
        {
            Id = reservation.Id,
            Destination = reservation.Destination,
            Duration = reservation.Duration,
            Origin = reservation.Origin,
            Price = reservation.Price,
            PassengersCount = reservation.PassengersCount,
            RideStatus = reservation.RideStatus,
            UserId = reservation.UserId,
            TaxiId = reservation.TaxiId,
            CreatedBy = reservation.CreatedBy,
            CreatedOn = reservation.CreatedOn,
            LastModifiedBy = reservation.LastModifiedBy,
            LastModifiedOn = reservation.LastModifiedOn
        };

        return model;
    }

    public async Task AddReservationAsync(ReservationCreateViewModel reservationViewModel)
    {
        var user = _contextAccessor.HttpContext.User.Identity?.Name;

        if (user != null)
        {
            var reservation = new Reservation
            {
                Id = reservationViewModel.Id,
                Destination = reservationViewModel.Destination,
                Duration = reservationViewModel.Duration,
                Origin = reservationViewModel.Origin,
                Price = reservationViewModel.Price,
                PassengersCount = reservationViewModel.PassengersCount,
                RideStatus = reservationViewModel.RideStatus,
                UserId = reservationViewModel.UserId,
                TaxiId = reservationViewModel.TaxiId,

                CreatedBy = user,
                CreatedOn = reservationViewModel.CreatedOn,
                LastModifiedBy = user,
                LastModifiedOn = DateTime.Now
            };

            await _reservationRepository.AddReservationAsync(reservation);
        }
    }

    public async Task UpdateReservationAsync(ReservationEditViewModel reservationViewModel)
    {
        var user = _contextAccessor.HttpContext.User.Identity?.Name;

        if (user != null)
        {
            var reservation = new Reservation
            {
                Id = reservationViewModel.Id,
                Destination = reservationViewModel.Destination,
                Duration = reservationViewModel.Duration,
                Origin = reservationViewModel.Origin,
                Price = reservationViewModel.Price,
                PassengersCount = reservationViewModel.PassengersCount,
                RideStatus = reservationViewModel.RideStatus,
                UserId = reservationViewModel.UserId,
                TaxiId = reservationViewModel.TaxiId,

                CreatedBy = reservationViewModel.CreatedBy,
                CreatedOn = reservationViewModel.CreatedOn,
                LastModifiedBy = user,
                LastModifiedOn = DateTime.Now
            };
        
            await _reservationRepository.UpdateReservationAsync(reservation);
        }
    }

    public async Task DeleteReservationAsync(int reservationId)
    {
        await _reservationRepository.DeleteReservationAsync(reservationId);
    }
}