using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CabTap.Core.Entities.Enums;

namespace CabTap.Core.Entities;

public class Taxi
{
    [Key]
    public int Id { get; set; }

    //use regex to validate format
    [Required]
    public string RegNumber { get; set; } = null!;
    
    [Required]
    public string Brand { get; set; } = null!;

    [ForeignKey(nameof(Category))]
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    [ForeignKey(nameof(Driver))]
    public string DriverId { get; set; } = null!;
    public Driver Driver { get; set; } = null!;

    //what
       
    public string Description { get; set; } = null!;

    [Required]
    [DataType(DataType.ImageUrl)]
    public string? Picture { get; set; }

    [Display(Name = "Status")]
    public TaxiStatus TaxiStatus { get; set; }

    public int PassengerSeats { get; set; }
}