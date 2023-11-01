using System.ComponentModel.DataAnnotations;

namespace Medicoz.Models
{
    public class TermandCondition
    {
        public int Id { get; set; }
        [StringLength(maximumLength:20)]
        public string Name { get; set; }
        [StringLength(maximumLength: 200)]

        public string Desc { get; set; }
        public bool IsDeleted { get; set; }
    }
}
