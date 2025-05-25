using System.ComponentModel.DataAnnotations;

namespace CabTap.Core.Entities;

public abstract class BaseEntity
{
    [DataType(DataType.DateTime)]
    public DateTime CreatedOn { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime LastModifiedOn { get; set; }
    
    public string CreatedBy { get; set; } = null!;
    
    public string LastModifiedBy { get; set; } = null!;
}