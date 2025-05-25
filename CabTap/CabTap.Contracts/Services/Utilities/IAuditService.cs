using CabTap.Core.Entities;

namespace CabTap.Contracts.Services.Utilities;

public interface IAuditService
{
    void SetCreationAuditInfo(BaseEntity entity, string userName);
    void SetModificationAuditInfo(BaseEntity entity, string userName);
}