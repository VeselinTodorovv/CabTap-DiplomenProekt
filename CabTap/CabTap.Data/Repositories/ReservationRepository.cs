using CabTap.Contracts.Repositories;
using CabTap.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CabTap.Data.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly ApplicationDbContext _context;
    
    public ReservationRepository(ApplicationDbContext context)
    {
        _context = context;
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
    
    public async Task<IEnumerable<Reservation>> GetPaginatedReservationsAsync(int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;
        return await _context.Reservations
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetPaginatedReservationsByUserIdAsync(string userId, int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;
        return await _context.Reservations
            .Where(r => r.UserId == userId)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();
    }

}