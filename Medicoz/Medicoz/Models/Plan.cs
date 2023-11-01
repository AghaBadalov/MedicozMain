using Medicoz.Enums;
using System.ComponentModel.DataAnnotations;

namespace Medicoz.Models
{
    public class Plan
    {
        public int Id { get; set; }
        public int PlanCategoryId { get; set; }
        public double Price { get; set; }
        public bool IsDeleted { get; set; }
        public Period Planperiod { get; set; }
        [StringLength(maximumLength:22)]
        public string Feature1 { get; set; }
        [StringLength(maximumLength: 22)]

        public string Feature2 { get; set; }
        [StringLength(maximumLength: 22)]

        public string Feature3 { get; set; }
        [StringLength(maximumLength: 22)]

        public string Feature4 { get; set; }
        [StringLength(maximumLength: 12)]

        
        public PlanCategory? PlanCategory { get; set; }
    }
}
