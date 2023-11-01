using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medicoz.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [StringLength(maximumLength:30)]
        public string Tittle { get; set; }
        [StringLength(maximumLength: 30)]

        public string Tittle2 { get; set; }
        [StringLength(maximumLength: 50)]

        public string Desc { get; set; }
        
        [StringLength(maximumLength: 130)]
        public string? ImageUrl { get; set; }
        public bool IsDeleted { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
