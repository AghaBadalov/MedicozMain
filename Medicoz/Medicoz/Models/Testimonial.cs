using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medicoz.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        [StringLength(maximumLength:25)]
        public string Name { get; set; }
        [StringLength(maximumLength:25)]
        public string Position { get; set; }
        [StringLength(maximumLength:101)]
        public string? ImageUrl { get; set; }
        [StringLength(maximumLength: 201)]

        public string Desc { get; set; }
        public bool IsDeleted { get; set; }
        [NotMapped]
        public IFormFile? ImageFIle { get; set; }
    }
}
