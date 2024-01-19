using CabTap.Shared.Reservation;

namespace CabTap.Contracts.Services;

public interface IReservationService
{
    Task<IEnumerable<ReservationAllViewModel>> GetAllReservationsAsync();
    Task<ReservationDetailsViewModel> GetReservationByIdAsync(int reservationId);
    Task AddReservationAsync(ReservationCreateViewModel reservationViewModel);
    Task UpdateReservationAsync(ReservationEditViewModel reservationViewModel);
    Task DeleteReservationAsync(int reservationId);
}