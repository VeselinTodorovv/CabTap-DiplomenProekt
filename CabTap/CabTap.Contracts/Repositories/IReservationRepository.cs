using CabTap.Core.Entities;

namespace CabTap.Contracts.Repositories;

public interface IReservationRepository
{
    IQueryable<Reservation> GetReservationsQuery(string userId, string searchInput);
    Task<Reservation> GetReservationByIdAsync(string reservationId);
    Task AddReservationAsync(Reservation reservation);
    Task UpdateReservationAsync(Reservation reservation);
    Task DeleteReservationAsync(string reservationId);
}