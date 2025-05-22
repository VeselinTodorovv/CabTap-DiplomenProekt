using CabTap.Core.Entities;

namespace CabTap.Contracts.Services.Utilities;

public interface IAuditService
{
    void UpdateAuditInfo(BaseEntity entity, DateTime dateTime, string userName);
}