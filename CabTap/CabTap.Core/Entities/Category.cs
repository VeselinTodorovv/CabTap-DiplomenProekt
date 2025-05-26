using System.ComponentModel.DataAnnotations;

namespace CabTap.Core.Entities;

public class Category
{
    public int Id { get; set; }

    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; } = null!;
    
    public decimal Rate { get; set; }
    
    public string? Image { get; set; }

    public virtual IEnumerable<Taxi> Taxis { get; set; } = new List<Taxi>();
}