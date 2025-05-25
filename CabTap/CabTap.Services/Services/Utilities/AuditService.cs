using CabTap.Contracts.Services.Utilities;
using CabTap.Core.Entities;

namespace CabTap.Services.Services.Utilities;

public class AuditService : IAuditService
{
    private readonly IDateTimeService _dateTimeService;
    public AuditService(IDateTimeService dateTimeService)
    {
        _dateTimeService = dateTimeService;
    }

    public void SetCreationAuditInfo(BaseEntity entity, string user)
    {
        var timestamp = _dateTimeService.GetCurrentDateTime();
        
        entity.CreatedBy = user;
        entity.CreatedOn = timestamp;
        
        entity.LastModifiedBy = user;
        entity.LastModifiedOn = timestamp;
    }

    public void SetModificationAuditInfo(BaseEntity entity, string user)
    {
        var timestamp = _dateTimeService.GetCurrentDateTime();
        
        entity.LastModifiedBy = user;
        entity.LastModifiedOn = timestamp;
    }
}