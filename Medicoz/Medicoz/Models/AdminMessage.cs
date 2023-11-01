using System.ComponentModel.DataAnnotations;

namespace Medicoz.Models
{
    public class AdminMessage
    {
        public int Id { get; set; }
        [StringLength(maximumLength:40)]
        public string UserName { get; set; }
        [StringLength(maximumLength:100),DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [StringLength(maximumLength: 20), DataType(DataType.PhoneNumber)]

        public string Phone { get; set; }
        [StringLength(maximumLength:200)]
        public string Message { get; set; }
        public DateTime MessageTime { get; set; }
    }
}
