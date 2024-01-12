using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using WebTaxiApp.Infrastructure.Data.Domain.Enum;

namespace WebTaxiApp.Infrastructure.Data.Domain
{
    public class Reservation
    {
        [Key]
        public string Id { get; set; } = null!;

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        [ForeignKey(nameof(Taxi))]
        public int TaxiId { get; set; }
        public Taxi Taxi { get; set; } = null!;

        [DataType(DataType.DateTime)]
        //TODO: test to see if the format displays as intended
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy hh:mm tt}")]
        [Display(Name = "Time and Date of Reservation")]
        public DateTime ReservationTime { get; set; }

        [Required]
        //start location
        public string Origin { get; set; } = null!;

        [Required]
        public string Destination { get; set; } = null!;

        [Display(Name = "Passengers Count")]
        //TODO: Check if this value is less or equal to the taxi's max seats
        public int PassengersCount { get; set; }

        [Required]
        public RideStatus RideStatus { get; set; }
    }
}
