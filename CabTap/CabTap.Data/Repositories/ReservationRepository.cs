using CabTap.Contracts.Repositories.Reservation;
using CabTap.Core.Entities;

namespace CabTap.Data.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly ApplicationDbContext _context;
    
    public ReservationRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IQueryable<Reservation> GetReservationsQuery(string? userId, string searchInput)
    {
        IQueryable<Reservation> query = _context.Reservations;

        // For MyReservations
        if (!string.IsNullOrEmpty(userId))
        {
            query = query.Where(r => string.Equals(r.UserId, userId));
        }
        // For Index
        else if (!string.IsNullOrWhiteSpace(searchInput))
        {
            query = query.Where(r => string.Equals(r.User.UserName, searchInput));
        }

        return query;
    }

    public async Task<Reservation> GetReservationByIdAsync(string reservationId)
    {
        var reservation = await _context.Reservations.FindAsync(reservationId);
        if (reservation == null)
        {
            throw new InvalidOperationException($"Reservation with id {reservationId} not found.");
        }
        
        return reservation;
    }

    public async Task AddReservationAsync(Reservation reservation)
    {
        if (reservation == null)
        {
            throw new InvalidOperationException("Reservation cannot be null.");
        }
        
        await _context.Reservations.AddAsync(reservation);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateReservationAsync(Reservation reservation)
    {
        if (reservation == null)
        {
            throw new InvalidOperationException("Reservation cannot be null.");
        }
        
        _context.Reservations.Update(reservation);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteReservationAsync(string reservationId)
    {
        var reservationToRemove = await _context.Reservations.FindAsync(reservationId);
        if (reservationToRemove == null)
        {
            throw new InvalidOperationException("Reservation not found.");
        }

        _context.Reservations.Remove(reservationToRemove);
        await _context.SaveChangesAsync();
    }
}