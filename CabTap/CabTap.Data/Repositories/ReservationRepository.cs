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
    
    public async Task<IEnumerable<Reservation>> GetPaginatedReservationsAsync(string sortOption, int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;
        var query = _context.Reservations
            .AsQueryable();
    
        query = ApplySorting(query, sortOption);

        return await query
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<IEnumerable<Reservation>> GetPaginatedReservationsByUserIdAsync(string userId, string sortOption, int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;
        var query = _context.Reservations
            .Where(r => r.UserId == userId)
            .AsQueryable();
    
        query = ApplySorting(query, sortOption);

        return await query.Skip(skip)
            .Take(pageSize)
            .ToListAsync();
    }

    private static IQueryable<Reservation> ApplySorting(IQueryable<Reservation> query, string sortOption)
    {
        return sortOption switch
        {
            "priceAsc" => query.OrderBy(x => x.Price),
            "priceDesc" => query.OrderByDescending(x => x.Price),
            "distanceAsc" => query.OrderBy(x => x.Distance),
            "distanceDesc" => query.OrderByDescending(x => x.Distance),
            "dateAsc" => query.OrderBy(x => x.ReservationDateTime),
            "dateDesc" => query.OrderByDescending(x => x.ReservationDateTime),
            "oldest" => query.OrderBy(r => r.CreatedOn),
            _ => query.OrderByDescending(r => r.LastModifiedOn)
        };
    }
}