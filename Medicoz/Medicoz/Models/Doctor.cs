using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Medicoz.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        [StringLength(maximumLength:50)]
        public string Name { get; set; }
        [StringLength(maximumLength: 50)]

        public string Desc { get; set; }
        [StringLength(maximumLength: 100)]

        public string Education { get; set; }
        [StringLength(maximumLength: 20)]

        public string Adress { get; set; }
        [StringLength(maximumLength: 100)]

        public string Experience { get; set; }
        [StringLength(maximumLength: 20)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [StringLength(maximumLength: 50)]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }
        [StringLength(maximumLength: 50)]

        public string Website { get; set; }
        [StringLength(maximumLength: 50)]

        public string? IGUrl { get; set; }
        [StringLength(maximumLength: 50)]

        public string? TTUrl { get; set; }
        [StringLength(maximumLength: 50)]

        public string? FBUrl { get; set; }
        [StringLength(maximumLength: 50)]

        public string? LNUrl { get; set; }
        [StringLength(maximumLength: 101)]

        public string? ImageUrl { get; set; }
        [DataType(DataType.Time)]
        public DateTime WorkStartTime { get; set; }
        [DataType(DataType.Time)]

        public DateTime WorkEndTime { get; set; }
        public bool IsDeleted { get; set; }

        public Department? Department { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }

    }
}
