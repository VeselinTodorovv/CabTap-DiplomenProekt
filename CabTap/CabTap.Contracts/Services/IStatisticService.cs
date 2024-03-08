namespace CabTap.Contracts.Services;

public interface IStatisticService
{
    // TODO: Make this more useful, not just a wrapper
    int CountTaxis();
    int CountDrivers();
    int CountClients();
    int CountReservations();
    int CountReservations(string userId);
    decimal SumReservations();
}