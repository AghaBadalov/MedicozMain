using System.ComponentModel.DataAnnotations;

namespace Medicoz.Areas.manage.ViewModels
{
    public class AdminLoginVM
    {
        [Required]
        [StringLength(maximumLength: 50)]
        public string UserName { get; set; }
        [Required]
        [StringLength (maximumLength: 50,MinimumLength =8),DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
