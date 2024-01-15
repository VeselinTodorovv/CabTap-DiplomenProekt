using System.ComponentModel.DataAnnotations;

namespace CabTap.Core.Entities;

public class Driver
{
    [Key]
    public string Id { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;
    
    public virtual IEnumerable<Taxi> Taxis { get; set; }
}