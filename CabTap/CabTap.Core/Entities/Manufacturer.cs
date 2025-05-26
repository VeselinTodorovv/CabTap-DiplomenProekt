using System.ComponentModel.DataAnnotations;

namespace CabTap.Core.Entities;

public class Manufacturer
{
    public int Id { get; set; }
    
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; } = null!;

    public virtual IEnumerable<Taxi> Taxis { get; } = new List<Taxi>();
}