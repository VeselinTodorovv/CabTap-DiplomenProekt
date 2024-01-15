using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CabTap.Shared.Authentication
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; } = null!;

        [Required]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}
