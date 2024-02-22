using CabTap.Contracts.Repositories;
using CabTap.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CabTap.Data.Repositories;

public class DriverRepository : IDriverRepository
{
    private readonly ApplicationDbContext _context;
    
    public DriverRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Driver?>> GetAllDriversAsync()
    {
        return await _context.Drivers.ToListAsync();
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
        await _context.Drivers.AddAsync(driver);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateDriverAsync(Driver driver)
    {
        var existingDriver = await _context.Drivers.FindAsync(driver.Id);

        if (existingDriver != null)
        {
            _context.Entry(existingDriver).CurrentValues.SetValues(driver);
            _context.Entry(existingDriver).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteDriverAsync(string driverId)
    {
        var driverToRemove = await _context.Drivers.FindAsync(driverId);
        if (driverToRemove != null)
        {
            _context.Drivers.Remove(driverToRemove);
            await _context.SaveChangesAsync();
        }
    }
}