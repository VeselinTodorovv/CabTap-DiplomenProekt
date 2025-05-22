using System.ComponentModel.DataAnnotations;

namespace CabTap.Core.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; } = null!;
    
    [Required]
    public decimal Rate { get; set; }
    
    public string? Image { get; set; }

    public virtual IEnumerable<Taxi> Taxis { get; set; } = new List<Taxi>();
}