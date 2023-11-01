using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medicoz.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        [StringLength(maximumLength:40)]
        public string Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedTime { get; set; }
        [StringLength(maximumLength: 101)]

        public string? ImageUrl { get; set; }
        [StringLength(maximumLength: 60)]

        public string TittleDesc { get; set; }
        [StringLength(maximumLength: 20)]

        public string AuthorName { get; set; }
        public int TotalView { get; set; }
        [StringLength(maximumLength: 100)]

        public string? FbUrl { get; set; }
        [StringLength(maximumLength: 100)]

        public string? TTUrl { get; set; }
        [StringLength(maximumLength: 100)]

        public string? IGUrl { get; set; }
        [StringLength(maximumLength: 400)]

        public string Desc1 { get; set; }
        [StringLength(maximumLength: 400)]

        public string Desc2 { get; set; }
        public List<BlogComment>? Comments { get; set; }


        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
