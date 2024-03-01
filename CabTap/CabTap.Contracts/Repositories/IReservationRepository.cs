using CabTap.Core.Entities;

namespace CabTap.Contracts.Repositories;

public interface IReservationRepository
{
    Task<IEnumerable<Reservation>> GetAllReservationsAsync();
    Task<Reservation> GetReservationByIdAsync(string reservationId);
    Task AddReservationAsync(Reservation reservation);
    Task UpdateReservationAsync(Reservation reservation);
    Task DeleteReservationAsync(string reservationId);
}