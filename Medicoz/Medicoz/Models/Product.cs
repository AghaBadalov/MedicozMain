using Medicoz.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medicoz.Models
{
    public class Product
    {
        public int Id { get; set; }
        public Shopstatus Status { get; set; }
        [StringLength(maximumLength:60)]
        public string  Name { get; set; }
        public double SalePrice { get; set; }
        public double DiscountPrice { get; set; }
        [StringLength(maximumLength: 100)]

        public string Desc { get; set; }
        [StringLength(maximumLength: 101)]

        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
