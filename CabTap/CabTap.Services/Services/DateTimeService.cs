using CabTap.Contracts.Services;

namespace CabTap.Services.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime GetCurrentDateTime()
    {
        return DateTime.Now;
    }
}