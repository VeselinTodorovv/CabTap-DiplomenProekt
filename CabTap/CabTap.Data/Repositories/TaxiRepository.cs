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

    public async Task<IEnumerable<Taxi?>> GetAllTaxisAsync()
    {
        return await _context.Taxis.ToListAsync();
    }

    public async Task<Taxi> GetTaxiByIdAsync(int taxiId)
    {
        return await _context.Taxis.FindAsync(taxiId);
    }

    public async Task AddTaxiAsync(Taxi taxi)
    {
        _context.Taxis.Add(taxi);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTaxiAsync(Taxi taxi)
    {
        var existingTaxi = await _context.Drivers.FindAsync(taxi.Id);

        if (existingTaxi != null)
        {
            _context.Entry(existingTaxi).CurrentValues.SetValues(taxi);
            _context.Entry(existingTaxi).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteTaxiAsync(int taxiId)
    {
        var taxiToRemove = await _context.Taxis.FindAsync(taxiId);
        if (taxiToRemove != null)
        {
            _context.Taxis.Remove(taxiToRemove);
            await _context.SaveChangesAsync();
        }
    }
}