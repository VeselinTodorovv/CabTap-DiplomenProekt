namespace CabTap.Contracts.Repositories;

public interface IStatisticRepository
{
    int CountTaxis();
    int CountDrivers();
    int CountClients();
    int CountReservations();
    int CountReservations(string userId);
    decimal SumReservations();
}