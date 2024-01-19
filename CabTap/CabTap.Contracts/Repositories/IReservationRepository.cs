using CabTap.Core.Entities;

namespace CabTap.Contracts.Repositories;

public interface IReservationRepository
{
    Task<IEnumerable<Reservation?>> GetAllReservationsAsync();
    Task<Reservation> GetReservationByIdAsync(int reservationId);
    Task AddReservationAsync(Reservation reservation);
    Task UpdateReservationAsync(Reservation reservation);
    Task DeleteReservationAsync(int reservationId);
}