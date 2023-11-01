using System.ComponentModel.DataAnnotations;

namespace Medicoz.Models
{
    public class Feature
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 101)]
        public string Icon { get; set; }
        [StringLength(maximumLength: 20)]

        public string Tittle { get; set; }
        [StringLength(maximumLength: 101)]

        public string Desc { get; set; }
        public bool IsDeleted { get; set; }
    }
}
