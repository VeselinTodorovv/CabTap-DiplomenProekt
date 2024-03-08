using System.ComponentModel.DataAnnotations;

namespace CabTap.Core.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public decimal Rate { get; set; }

    public virtual ICollection<Taxi> Taxis { get; set; } = new List<Taxi>();
}