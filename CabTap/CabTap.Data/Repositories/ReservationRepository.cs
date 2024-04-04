using CabTap.Contracts.Repositories;
using CabTap.Core.Entities;
using CabTap.Core.Entities.Enums;

namespace CabTap.Data.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly ApplicationDbContext _context;
    
    public ReservationRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public IQueryable<Reservation> GetPaginatedReservationsQuery(string userId, string searchInput, string sortOption, string reservationType)
    {
        IQueryable<Reservation> query = _context.Reservations;

        // For MyReservations
        if (!string.IsNullOrEmpty(userId))
        {
            query = query.Where(r => r.UserId == userId);
        }

        if (!string.IsNullOrWhiteSpace(searchInput))
        {
            query = query.Where(r => r.User.UserName == searchInput);
        }

        query = ApplyFiltering(query, reservationType);
        query = ApplySorting(query, sortOption);

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

    private static IQueryable<Reservation> ApplyFiltering(IQueryable<Reservation> query, string reservationType)
    {
        return !string.IsNullOrEmpty(reservationType)
            ? query.Where(r => r.ReservationType == Enum.Parse<ReservationType>(reservationType))
            : query;
    }
}