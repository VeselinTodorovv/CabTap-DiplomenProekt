using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CabTap.Core.Entities;

public class Driver : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Name { get; set; } = null!;
    
    public virtual IEnumerable<Taxi> Taxis { get; set; } = new List<Taxi>();
}