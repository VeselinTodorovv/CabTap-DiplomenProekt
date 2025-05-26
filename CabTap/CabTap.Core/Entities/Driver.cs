namespace CabTap.Core.Entities;

public class Driver : BaseEntity
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;
    
    public virtual IEnumerable<Taxi> Taxis { get; set; } = new List<Taxi>();
}