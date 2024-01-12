using System.ComponentModel.DataAnnotations;

namespace WebTaxiApp.Infrastructure.Data.Domain.Enum
{
    public enum RideStatus : byte
    {
        [Display(Name = "In Progress")]
        InProgress,
        Finished,
        Cancelled
    }
}
