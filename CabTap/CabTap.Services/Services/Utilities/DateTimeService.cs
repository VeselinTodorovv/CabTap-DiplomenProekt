using CabTap.Contracts.Services.Utilities;

namespace CabTap.Services.Services.Utilities;

public class DateTimeService : IDateTimeService
{
    public DateTime GetCurrentDateTime()
    {
        return DateTime.Now;
    }
}