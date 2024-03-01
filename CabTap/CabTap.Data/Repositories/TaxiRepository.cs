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
        var existingTaxi = await _context.Taxis.FindAsync(taxi.Id);
        if (existingTaxi == null)
        {
            throw new InvalidOperationException("Taxi not found");
        }

        _context.Entry(existingTaxi).CurrentValues.SetValues(taxi);
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