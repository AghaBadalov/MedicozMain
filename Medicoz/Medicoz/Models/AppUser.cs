using Microsoft.AspNetCore.Identity;

namespace Medicoz.Models
{
    public class AppUser :IdentityUser
    {
        public string FullName { get; set; }
    }
}
