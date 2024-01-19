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

    public async Task<Driver> GetDriverByIdAsync(int driverId)
    {
        return await _context.Drivers.FindAsync(driverId);
    }

    public async Task AddDriverAsync(Driver driver)
    {
        _context.Drivers.Add(driver);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateDriverAsync(Driver driver)
    {
        var existingDrier = await _context.Drivers.FindAsync(driver.Id);

        if (existingDrier != null)
        {
            _context.Entry(existingDrier).CurrentValues.SetValues(driver);
            _context.Entry(existingDrier).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteDriverAsync(int driverId)
    {
        var driverToRemove = await _context.Drivers.FindAsync(driverId);
        if (driverToRemove != null)
        {
            _context.Drivers.Remove(driverToRemove);
            await _context.SaveChangesAsync();
        }
    }
}