using CabTap.Contracts.Repositories;
using CabTap.Contracts.Services;

namespace CabTap.Services.Services;

public class StatisticService : IStatisticService
{
    private readonly IStatisticRepository _statisticRepository;

    public StatisticService(IStatisticRepository statisticRepository)
    {
        _statisticRepository = statisticRepository;
    }

    public int CountTaxis()
    {
        return _statisticRepository.CountTaxis();
    }

    public int CountDrivers()
    {
        return _statisticRepository.CountDrivers();
    }

    public int CountClients()
    {
        return _statisticRepository.CountClients();
    }

    public int CountReservations()
    {
        return _statisticRepository.CountReservations();
    }

    public decimal SumReservations()
    {
        return _statisticRepository.SumReservations();
    }
}