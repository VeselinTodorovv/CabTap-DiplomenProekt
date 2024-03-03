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
    
    public async Task<IEnumerable<Reservation>> GetAllReservationsAsync()
    {
        return await _context.Reservations.ToListAsync();
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
        await _context.Reservations.AddAsync(reservation);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateReservationAsync(Reservation reservation)
    {
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