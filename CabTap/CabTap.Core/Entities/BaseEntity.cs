using System.ComponentModel.DataAnnotations;

namespace CabTap.Core.Entities;

public abstract class BaseEntity
{
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime CreatedOn { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime LastModifiedOn { get; set; }
    
    [Required]
    public string CreatedBy { get; set; } = null!;
    
    [Required]
    public string LastModifiedBy { get; set; } = null!;

    public void UpdateAuditInfo(string userName)
    {
        if (string.IsNullOrEmpty(CreatedBy))
        {
            CreatedBy = userName;
            CreatedOn = DateTime.UtcNow;
        }
        
        LastModifiedBy = userName;
        LastModifiedOn = DateTime.UtcNow;
    }
}