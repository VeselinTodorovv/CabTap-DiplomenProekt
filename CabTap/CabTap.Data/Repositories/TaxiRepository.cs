using CabTap.Contracts.Repositories;
using CabTap.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CabTap.Data.Repositories;

public class TaxiRepository : ITaxiRepository
{
    private readonly ApplicationDbContext _context;
    
    public TaxiRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Taxi>> GetAllTaxisAsync()
    {
        return await _context.Taxis.ToListAsync();
    }

    public async Task<IEnumerable<Taxi>> GetPaginatedReservationsAsync(int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;
        return await _context.Taxis
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<Taxi> GetTaxiByIdAsync(int taxiId)
    {
        var taxi = await _context.Taxis.FindAsync(taxiId);
        if (taxi == null)
        {
            throw new InvalidOperationException($"Taxi with id {taxiId} not found.");
        }
        
        return taxi;
    }

    public async Task AddTaxiAsync(Taxi taxi)
    {
        await _context.Taxis.AddAsync(taxi);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTaxiAsync(Taxi taxi)
    {
        _context.Taxis.Update(taxi);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTaxiAsync(int taxiId)
    {
        var taxiToRemove = await _context.Taxis.FindAsync(taxiId);
        if (taxiToRemove == null)
        {
            throw new InvalidOperationException("Taxi not found");
        }

        _context.Taxis.Remove(taxiToRemove);
        await _context.SaveChangesAsync();
    }
}