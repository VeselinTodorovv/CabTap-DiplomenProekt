using System.ComponentModel.DataAnnotations;

namespace CabTap.Core.Entities;

public class Driver : BaseEntity
{
    [Key]
    public string Id { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;
    
    public virtual ICollection<Taxi> Taxis { get; set; }
}