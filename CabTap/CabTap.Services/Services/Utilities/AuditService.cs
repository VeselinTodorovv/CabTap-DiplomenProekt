using CabTap.Contracts.Services.Utilities;
using CabTap.Core.Entities;

namespace CabTap.Services.Services.Utilities;

public class AuditService : IAuditService
{
    public void UpdateAuditInfo(BaseEntity entity, DateTime dateTime, string userName)
    {
        if (string.IsNullOrEmpty(entity.CreatedBy))
        {
            entity.CreatedBy = userName;
            entity.CreatedOn = dateTime;
        }
        
        entity.LastModifiedBy = userName;
        entity.LastModifiedOn = dateTime;
    }
}