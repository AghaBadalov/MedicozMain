using Medicoz.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Medicoz.ViewModels
{
    public class BlogVM
    {
        public BlogPost? Post { get; set; }
        public BlogComment? Comments { get; set; }
        [StringLength(maximumLength:30,MinimumLength =3)]
        public string UserName { get; set; }
        [StringLength(maximumLength: 30,MinimumLength =3)]
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }
    }
}
