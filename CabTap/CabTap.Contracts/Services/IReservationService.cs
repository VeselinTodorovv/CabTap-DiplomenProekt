using CabTap.Shared.Reservation;

namespace CabTap.Contracts.Services;

public interface IReservationService
{
    Task<IEnumerable<ReservationAllViewModel>> GetPaginatedReservationsAsync(string searchInput, string sortOption, string reservationType, int page, int pageSize);
    Task<IEnumerable<ReservationAllViewModel>> GetPaginatedReservationsByUserNameAsync(string searchInput, string sortOption, string reservationType, int page, int pageSize);
    Task<ReservationDetailsViewModel> GetReservationByIdAsync(string reservationId);
    Task MarkAsCompleted(string reservationId);
    Task MarkAsCanceled(string reservationId);
    Task AddReservationAsync(ReservationCreateViewModel reservationViewModel);
    Task UpdateReservationAsync(ReservationEditViewModel reservationViewModel);
    Task DeleteReservationAsync(string reservationId);
}