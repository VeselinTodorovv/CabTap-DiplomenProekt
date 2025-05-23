using CabTap.Contracts.Services.Utilities;
using CabTap.Core.Entities;

namespace CabTap.Services.Services.Utilities;

public class AuditService : IAuditService
{
    public void UpdateAuditInfo(BaseEntity entity, DateTime timestamp, string user)
    {
        var isNewEntity = string.IsNullOrEmpty(entity.CreatedBy);
    
        if (isNewEntity)
        {
            entity.CreatedBy = user;
            entity.CreatedOn = timestamp;
        }
        
        entity.LastModifiedBy = user;
        entity.LastModifiedOn = timestamp;

        entity.CreatedOn = EnsureDateTimeIsUtc(entity.CreatedOn);
        entity.LastModifiedOn = EnsureDateTimeIsUtc(entity.LastModifiedOn);
    }

    private static DateTime EnsureDateTimeIsUtc(DateTime dateTime)
    {
        return dateTime.Kind == DateTimeKind.Unspecified
            ? DateTime.SpecifyKind(dateTime, DateTimeKind.Utc)
            : dateTime;
    }
}