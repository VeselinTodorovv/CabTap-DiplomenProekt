using CabTap.Contracts.Repositories.Taxi;
using CabTap.Core.Entities;

namespace CabTap.Data.Repositories.Taxi;

public class DriverRepository : IDriverRepository
{
    private readonly ApplicationDbContext _context;
    
    public DriverRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Driver> GetDriversQuery()
    {
        return _context.Drivers;
    }

    public async Task<Driver> GetDriverByIdAsync(string driverId)
    {
        var driver = await _context.Drivers.FindAsync(driverId);
        if (driver == null)
        {
            throw new InvalidOperationException($"Driver with id {driverId} not found.");
        }

        return driver;
    }

    public async Task AddDriverAsync(Driver driver)
    {
        if (driver == null)
        {
            throw new InvalidOperationException("Driver cannot be null.");
        }
        
        await _context.Drivers.AddAsync(driver);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateDriverAsync(Driver driver)
    {
        if (driver == null)
        {
            throw new InvalidOperationException("Driver cannot be null.");
        }
        
        _context.Drivers.Update(driver);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteDriverAsync(string driverId)
    {
        var driverToRemove = await _context.Drivers.FindAsync(driverId);
        if (driverToRemove == null)
        {
            throw new InvalidOperationException("Driver not found");
        }

        _context.Drivers.Remove(driverToRemove);
        await _context.SaveChangesAsync();
    }
}