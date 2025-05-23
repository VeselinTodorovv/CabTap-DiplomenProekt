using CabTap.Core.Entities;

namespace CabTap.Contracts.Services.Utilities;

public interface IAuditService
{
    void SetCreationAuditInfo(BaseEntity entity, DateTime dateTime, string userName);
    void SetModificationAuditInfo(BaseEntity entity, DateTime dateTime, string userName);
}