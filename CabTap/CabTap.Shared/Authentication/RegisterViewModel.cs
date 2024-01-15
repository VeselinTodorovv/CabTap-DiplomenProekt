using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CabTap.Shared.Authentication
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string UserName { get; init; } = null!;

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; init; } = null!;

        [Required]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string Password { get; init; } = null!;

        [Required]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; init; } = null!;

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; init; } = null!;

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string LastName { get; init; } = null!;

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; init; } = null!;

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Address { get; init; } = null!;
    }
}
