using System.ComponentModel.DataAnnotations;

namespace Medicoz.Models
{
    public class PlanCategory
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

        [StringLength(maximumLength:25)]
        public string Name { get; set; }
        public List<Plan>? Plans { get; set; }
    }
}
