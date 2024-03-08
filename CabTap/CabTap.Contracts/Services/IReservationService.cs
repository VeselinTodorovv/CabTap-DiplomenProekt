using CabTap.Shared.Reservation;

namespace CabTap.Contracts.Services;

public interface IReservationService
{
    Task<IEnumerable<ReservationAllViewModel>> GetAllReservationsAsync();
    Task<ReservationDetailsViewModel> GetReservationByIdAsync(string reservationId);
    Task<IEnumerable<ReservationAllViewModel>> GetReservationsByUserIdAsync();
    Task AddReservationAsync(ReservationCreateViewModel reservationViewModel);
    Task UpdateReservationAsync(ReservationEditViewModel reservationViewModel);
    Task DeleteReservationAsync(string reservationId);

    Task<IEnumerable<ReservationAllViewModel>> GetPaginatedReservationsAsync(int page, int pageSize);
    Task<IEnumerable<ReservationAllViewModel>> GetPaginatedReservationsByUserIdAsync(int page, int pageSize);
}