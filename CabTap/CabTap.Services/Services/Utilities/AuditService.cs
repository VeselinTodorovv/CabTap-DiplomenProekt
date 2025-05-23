using CabTap.Contracts.Services.Utilities;
using CabTap.Core.Entities;

namespace CabTap.Services.Services.Utilities;

public class AuditService : IAuditService
{
    public void SetCreationAuditInfo(BaseEntity entity, DateTime timestamp, string user)
    {
        entity.CreatedBy = user;
        entity.CreatedOn = timestamp;
        
        entity.LastModifiedBy = user;
        entity.LastModifiedOn = timestamp;
    }

    public void SetModificationAuditInfo(BaseEntity entity, DateTime timestamp, string user)
    {
        entity.LastModifiedBy = user;
        entity.LastModifiedOn = timestamp;
    }
}