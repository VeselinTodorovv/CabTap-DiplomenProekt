using CabTap.Core.Entities;

namespace CabTap.Contracts.Repositories;

public interface IReservationRepository
{
    Task<Reservation> GetReservationByIdAsync(string reservationId);
    Task AddReservationAsync(Reservation reservation);
    Task UpdateReservationAsync(Reservation reservation);
    Task DeleteReservationAsync(string reservationId);

    Task<IEnumerable<Reservation>> GetPaginatedReservationsAsync(string sortOption, int page, int pageSize);
    Task<IEnumerable<Reservation>> GetPaginatedReservationsByUserIdAsync(string userId, string sortOption, int page, int pageSize);
}