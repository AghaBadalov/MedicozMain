using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medicoz.Models
{
    public class Settings
    {
        public int Id { get; set; }
        [StringLength(maximumLength:50)]
        public string? Key { get; set; }
        [StringLength(maximumLength: 150)]

        public string? Value { get; set; }
        [StringLength(maximumLength:101)]
        public string? ImageUrl { get; set; }
        public bool IsImage { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
