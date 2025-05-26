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
    [StringLength(50, MinimumLength = 3)]
    public string CreatedBy { get; set; } = null!;
    
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string LastModifiedBy { get; set; } = null!;
}