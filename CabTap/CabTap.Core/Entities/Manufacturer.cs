using System.ComponentModel.DataAnnotations;

namespace CabTap.Core.Entities;

public class Manufacturer
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; } = null!;

    public virtual ICollection<Taxi> Taxis { get; set; } = new List<Taxi>();
}