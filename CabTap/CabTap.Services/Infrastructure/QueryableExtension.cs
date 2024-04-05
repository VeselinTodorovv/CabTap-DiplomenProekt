using Microsoft.EntityFrameworkCore;

namespace CabTap.Services.Infrastructure;

public static class QueryableExtension
{
    public static async Task<IEnumerable<T>> PaginateAsync<T>(this IQueryable<T> query, int page, int pageSize)
    {
        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}