using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medicoz.Models
{
    public class About
    {
        public int Id { get; set; }
        [StringLength(maximumLength:40)]
        public string Tittle { get; set; }
        [StringLength(maximumLength: 140)]

        public string Desc { get; set; }
        [StringLength(maximumLength: 101)]


        public string? BigImageUrl { get; set; }
        [StringLength(maximumLength: 101)]

        public string? MiddleImageUrl { get; set; }
        [StringLength(maximumLength: 101)]

        public string VideoUrl { get; set; }
        [StringLength(maximumLength: 101)]

        public string? SmallImageUrl { get; set; }
        [NotMapped]
        public IFormFile? BigImage { get; set; }
        [NotMapped]
        public IFormFile? MiddleImage { get; set; }
        [NotMapped]
        public IFormFile? SmallImage { get; set; }
    }
}
