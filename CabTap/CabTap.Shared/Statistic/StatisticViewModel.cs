using System.ComponentModel.DataAnnotations;

namespace CabTap.Shared.Statistic;

public class StatisticViewModel
{
    [Display(Name = "Taxis Count")]
    public int CountTaxis { get; set; }
    
    [Display(Name = "Drivers Count")]
    public int CountDrivers { get; set; }
    
    [Display(Name = "Clients Count")]
    public int CountClients { get; set; }
    
    [Display(Name = "Reservations Count")]
    public int CountReservations { get; set; }
    
    [Display(Name = "Reservations Sum")]
    public decimal SumReservations { get; set; }
}