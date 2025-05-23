using CabTap.Contracts.Services.Utilities;

namespace CabTap.Services.Services.Utilities;

public class DateTimeService : IDateTimeService
{
    public DateTime GetCurrentDateTime()
    {
        var currentDateTime = DateTime.UtcNow;
        currentDateTime = DateTime.SpecifyKind(currentDateTime, DateTimeKind.Utc);
        
        return currentDateTime;
    }
}