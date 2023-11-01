using Medicoz.DAL;
using Medicoz.Models;
using Microsoft.AspNetCore.Identity;

namespace Medicoz.Services
{
    public class UserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public UserService(IHttpContextAccessor contextAccessor,UserManager<AppUser> userManager)
        {
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }
        public async Task<AppUser> GetUser()
        {
            AppUser user = null;
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                user =await _userManager.FindByNameAsync(_contextAccessor.HttpContext.User.Identity.Name);
                return user;
            }
            
            return null;
        }
    }
}
