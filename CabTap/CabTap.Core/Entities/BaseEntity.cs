using System.ComponentModel.DataAnnotations;

namespace CabTap.Core.Entities;

public abstract class BaseEntity
{
    // TODO: Consider implementing a DateTime service
    
    [Required]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{dd/MM/yyyy hh:mm tt}")]
    public DateTime CreatedOn { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{dd/MM/yyyy hh:mm tt}")]
    public DateTime LastModifiedOn { get; set; }
    
    [Required]
    public string CreatedBy { get; set; } = null!;
    
    [Required]
    public string LastModifiedBy { get; set; } = null!;
}