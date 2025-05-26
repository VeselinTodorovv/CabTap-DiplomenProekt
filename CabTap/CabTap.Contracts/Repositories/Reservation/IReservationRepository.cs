namespace CabTap.Contracts.Repositories.Reservation;

using Reservation=Core.Entities.Reservation;

public interface IReservationRepository
{
    IQueryable<Reservation> GetReservationsQuery(string? userId, string searchInput);
    Task<Reservation> GetReservationByIdAsync(string reservationId);
    Task AddReservationAsync(Reservation reservation);
    Task UpdateReservationAsync(Reservation reservation);
    Task DeleteReservationAsync(string reservationId);
}