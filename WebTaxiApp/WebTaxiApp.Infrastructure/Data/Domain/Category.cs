using System.ComponentModel.DataAnnotations;

namespace WebTaxiApp.Infrastructure.Data.Domain
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}