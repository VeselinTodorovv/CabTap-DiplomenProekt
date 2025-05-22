namespace CabTap.Contracts.Repositories.Reservation;

public interface IReservationRepository
{
    IQueryable<Core.Entities.Reservation> GetReservationsQuery(string userId, string searchInput);
    Task<Core.Entities.Reservation> GetReservationByIdAsync(string reservationId);
    Task AddReservationAsync(Core.Entities.Reservation reservation);
    Task UpdateReservationAsync(Core.Entities.Reservation reservation);
    Task DeleteReservationAsync(string reservationId);
}