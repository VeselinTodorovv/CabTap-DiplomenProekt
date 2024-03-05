using CabTap.Contracts.Repositories;

namespace CabTap.Data.Repositories;

public class StatisticRepository : IStatisticRepository
{
    private readonly ApplicationDbContext _context;

    public StatisticRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public int CountTaxis()
    {
        return _context.Taxis.Count();
    }

    public int CountDrivers()
    {
        return _context.Drivers.Count();
    }

    public int CountClients()
    {    
        var adminRoleId = _context.Roles
            .Where(role => role.Name == "Administrator")
            .Select(role => role.Id)
            .FirstOrDefault();

        // Count of clients only
        var clientCount = _context.UserRoles
            .Count(userRole => userRole.RoleId != adminRoleId);

        return clientCount;
    }

    public int CountReservations()
    {
        return _context.Reservations.Count();
    }

    public decimal SumReservations()
    {
        return _context.Reservations.Sum(x => x.Price);
    }
}