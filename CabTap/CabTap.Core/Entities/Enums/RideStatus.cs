using System.ComponentModel.DataAnnotations;

namespace CabTap.Core.Entities.Enums;

public enum RideStatus
{
    [Display(Name = "In Progress")]
    InProgress = 1,
    Finished = 2,
    Canceled = 3
}