using CabTap.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CabTap.Data.Repositories;

public class StatisticRepository : IStatisticRepository
{
    private readonly ApplicationDbContext _context;

    public StatisticRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<int> CountTaxisAsync()
    {
        return _context.Taxis
            .CountAsync();
    }

    public Task<int> CountDriversAsync()
    {
        return _context.Drivers
            .CountAsync();
    }

    public async Task<int> CountClientsAsync()
    {    
        var adminRoleId = await _context.Roles
            .Where(role => role.Name == "Administrator")
            .Select(role => role.Id)
            .FirstOrDefaultAsync();

        // Count of clients only
        var clientCount = await _context.UserRoles
            .CountAsync(userRole => userRole.RoleId != adminRoleId);

        return clientCount;
    }

    public Task<int> CountReservationsAsync()
    {
        return _context.Reservations
            .CountAsync();
    }
    
    public Task<int> CountReservationsAsync(string userName)
    {
        return _context.Reservations
            .CountAsync(x => x.User.UserName == userName);
    }

    public Task<decimal> SumReservationsAsync()
    {
        return _context.Reservations
            .SumAsync(x => x.Price);
    }
}