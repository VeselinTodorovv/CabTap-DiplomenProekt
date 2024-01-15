using System.ComponentModel.DataAnnotations;

namespace CabTap.Core.Entities;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;

    public virtual IEnumerable<Taxi> Taxis { get; set; }
}