using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CabTap.Core.Entities;

public class Driver : BaseEntity
{
    public Driver()
    {
        Id = Guid.NewGuid().ToString();
    }
    
    [Key]
    public string Id { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;
    
    [ForeignKey(nameof(Taxi))]
    public int TaxiId { get; set; }
    public virtual Taxi Taxi { get; set; }
}