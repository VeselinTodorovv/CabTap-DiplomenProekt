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
    
    public async Task<IEnumerable<Reservation?>> GetAllReservationsAsync()
    {
        return await _context.Reservations.ToListAsync();
    }

    public async Task<Reservation> GetReservationByIdAsync(int reservationId)
    {
        return await _context.Reservations.FindAsync(reservationId);
    }

    public async Task AddReservationAsync(Reservation reservation)
    {
        await _context.Reservations.AddAsync(reservation);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateReservationAsync(Reservation reservation)
    {
        var existingReservation = await _context.Reservations.FindAsync(reservation.Id);
        if (existingReservation != null)
        {
            _context.Entry(existingReservation).CurrentValues.SetValues(reservation);
            _context.Entry(existingReservation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteReservationAsync(int reservationId)
    {
        var reservationToRemove = await _context.Reservations.FindAsync(reservationId);
        if (reservationToRemove != null)
        {
            _context.Reservations.Remove(reservationToRemove);
            await _context.SaveChangesAsync();
        }
    }
}