using System.ComponentModel.DataAnnotations.Schema;

namespace Medicoz.Models
{
    public class Gallery
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Tittle { get; set; }
        public string? Imageurl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
        public Department? Department { get; set; }
    }
}
