using CabTap.Core.Entities.Enums;

namespace CabTap.Services.Services.Reservation;

public static class ReservationQueries
{
    public static IQueryable<Core.Entities.Reservation> ApplySorting(IQueryable<Core.Entities.Reservation> query, string sortOption)
        => sortOption switch
        {
            "priceAsc" => query.OrderBy(x => x.Price),
            "priceDesc" => query.OrderByDescending(x => x.Price),
            "distanceAsc" => query.OrderBy(x => x.Distance),
            "distanceDesc" => query.OrderByDescending(x => x.Distance),
            "dateAsc" => query.OrderBy(x => x.ReservationDateTime),
            "dateDesc" => query.OrderByDescending(x => x.ReservationDateTime),
            "oldest" => query.OrderBy(r => r.CreatedOn),
            _ => query.OrderByDescending(r => r.LastModifiedOn)
        };

    public static IQueryable<Core.Entities.Reservation> ApplyFiltering(IQueryable<Core.Entities.Reservation> query, string reservationType) 
        => Enum.TryParse<ReservationType>(reservationType, out var type)
        ? query.Where(r => r.ReservationType == type)
        : query;
}