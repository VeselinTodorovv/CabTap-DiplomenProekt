using System.ComponentModel.DataAnnotations;

namespace WebTaxiApp.Infrastructure.Data.Domain
{
    public class Driver
    {
        [Key]
        public string Id { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;
    }
}