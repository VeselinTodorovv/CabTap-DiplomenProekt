using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CabTap.Core.Entities;

public class Driver : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id { get; set; } = null!;

    [Required]
    public string Name { get; set; } = null!;
    
    public virtual ICollection<Taxi> Taxis { get; set; } = new List<Taxi>();
}