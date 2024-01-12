using System.ComponentModel.DataAnnotations;

namespace WebTaxiApp.Infrastructure.Data.Domain
{
    public class Driver
    {
        [Key]
        public string Id { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        //TODO: decide if one driver can drive many taxis later
        public virtual IEnumerable<Driver> Drivers { get; set;}
    }
}