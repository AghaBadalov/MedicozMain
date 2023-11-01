using System.ComponentModel.DataAnnotations;

namespace Medicoz.ViewModels
{
    public class UserRegisterVM
    {
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string FullName { get; set; }

        [Required]
        [StringLength(maximumLength: 30, MinimumLength = 5)]
        public string UserName { get; set; }
        [Required]
        [StringLength(maximumLength: 50), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [StringLength(maximumLength: 40, MinimumLength = 8), DataType(DataType.Password)]
        public string Password { get; set; }
        [Compare(nameof(Password))]
        public string CheckPass { get; set; }
        [StringLength(25), DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
