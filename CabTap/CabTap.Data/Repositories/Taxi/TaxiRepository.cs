using CabTap.Contracts.Repositories.Taxi;

namespace CabTap.Data.Repositories.Taxi;
using Taxi=Core.Entities.Taxi;

public class TaxiRepository : ITaxiRepository
{
    private readonly ApplicationDbContext _context;
    
    public TaxiRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Taxi> GetTaxisQuery()
    {
        return _context.Taxis;
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
        if (taxi == null)
        {
            throw new InvalidOperationException("Taxi cannot be null.");
        }
        
        await _context.Taxis.AddAsync(taxi);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTaxiAsync(Taxi taxi)
    {
        if (taxi == null)
        {
            throw new InvalidOperationException("Taxi cannot be null.");
        }
        
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