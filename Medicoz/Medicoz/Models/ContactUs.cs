using System.ComponentModel.DataAnnotations;

namespace Medicoz.Models
{
    public class ContactUs
    {
        public int Id { get; set; }
        [StringLength(maximumLength:100),DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength (maximumLength:20),DataType(DataType.PhoneNumber)]
        public string Phone1 { get; set; }
        [StringLength(maximumLength: 20), DataType(DataType.PhoneNumber)]
        public string? Phone2 { get; set; }
        [StringLength(maximumLength:120)]
        public string Adress { get; set; }
    }
}
