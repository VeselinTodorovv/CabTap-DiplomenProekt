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

    public Task<int> CountTaxisAsync()
    {
        return _statisticRepository.CountTaxisAsync();
    }

    public Task<int> CountDriversAsync()
    {
        return _statisticRepository.CountDriversAsync();
    }

    public Task<int> CountClientsAsync()
    {
        return _statisticRepository.CountClientsAsync();
    }

    public Task<int> CountReservationsAsync()
    {
        return _statisticRepository.CountReservationsAsync();
    }

    public Task<int> CountReservationsAsync(string userName)
    {
        return _statisticRepository.CountReservationsAsync(userName);
    }

    public Task<decimal> SumReservationsAsync()
    {
        return _statisticRepository.SumReservationsAsync();
    }
}