using System.ComponentModel.DataAnnotations;

namespace CabTap.Shared.Category;

public class CategoryPairViewModel
{
    public int Id { get; set; }

    [Display(Name = "Category")]
    public string Name { get; set; } = null!;
}