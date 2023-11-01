using System.ComponentModel.DataAnnotations;

namespace Medicoz.Models
{
    public class FunFact
    {
        public int Id { get; set; }
        [StringLength(maximumLength:20)]
        public string Tittle { get; set; }
        [StringLength(maximumLength: 100)]

        public string Icon { get; set; }

    }
}
