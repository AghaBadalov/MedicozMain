using Medicoz.Areas.manage.ViewModels;
using Medicoz.DAL;
using Medicoz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Medicoz.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles =("SuperAdmin,Admin"))]
    public class DashboardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public DashboardController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public IActionResult Index()
        {
            DashboardVM dashboardVM = new DashboardVM
            {
                Messages = _context.Messages.ToList(),
            };
            return View(dashboardVM);
        }
        //public async Task<IActionResult> CreateAdmin()
        //{
        //    AppUser appuser = new AppUser
        //    {
        //        FullName = "Murad Bayramov",
        //        UserName = "Muriko"
        //    };
        //    await _userManager.CreateAsync(appuser, "Muriko123");
        //    return Ok("admin created1");
        // }
        //public async Task<IActionResult> Createrole()
        //{
        //    IdentityRole role1=new IdentityRole("SuperAdmin");
        //    IdentityRole role2=new IdentityRole("Admin");
        //    IdentityRole role3=new IdentityRole("Member");
        //    await _roleManager.CreateAsync(role3);
        //    await _roleManager.CreateAsync(role2);

        //    await _roleManager.CreateAsync(role1);
        //    return Ok("RolesCreated");
        //}
        //public async Task<IActionResult> AddRole()
        //{
        //    AppUser admin =await _userManager.FindByNameAsync("Muriko");
        //    await _userManager.AddToRoleAsync(admin, "Admin");
        //    return Ok("Roleadded");
        //}
        
    }
}
