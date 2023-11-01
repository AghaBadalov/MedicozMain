using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medicoz.Models
{
    public class Department
    {
        public int Id { get; set; }
        [StringLength(maximumLength:30)]
        public string Name { get; set; }
        [StringLength(maximumLength:60)]
        public string Tittle { get; set; }

        [StringLength(maximumLength:400)]
        public string Description1 { get; set; }
        [StringLength(maximumLength:300)]
        public string? Description2 { get; set; }

        [StringLength(maximumLength: 100)]

        public string? Imageurl { get; set; }
        [StringLength(maximumLength:100)]
        public string IconUrl { get; set; }
        public List<Doctor>? Doctors { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}
