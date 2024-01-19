using System.ComponentModel.DataAnnotations;

namespace CabTap.Shared.Driver;

public class DriverAllViewModel
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;
    
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{dd/MM/yyyy hh:mm tt}")]
    public DateTime CreatedOn { get; set; }
    
    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{dd/MM/yyyy hh:mm tt}")]
    public DateTime LastModifiedOn { get; set; }
    
    public string CreatedBy { get; set; } = null!;
    
    public string LastModifiedBy { get; set; } = null!;
}