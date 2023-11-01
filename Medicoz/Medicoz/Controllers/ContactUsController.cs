using Medicoz.Areas.manage.ViewModels;
using Medicoz.DAL;
using Medicoz.Models;
using Medicoz.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Medicoz.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ContactUsController(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            AppUser member = null;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                member = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }
            ContactVM contactVM = new ContactVM
            {
                Email = member?.Email,
                Phone = member?.PhoneNumber,
                UserName = member?.UserName,

            };


            return View(contactVM);
        }
        [HttpPost]
        public  IActionResult Index(ContactVM contactvm)
        {
            if(!ModelState.IsValid) return View(contactvm);

            AdminMessage message = new AdminMessage();
            message.Message=contactvm.Message;
            message.Email = contactvm.Email;
            message.UserName = contactvm.UserName;
               message.Phone = contactvm.Phone;

           
            message.MessageTime = DateTime.Now;
            _context.Messages.Add(message);
            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }
}
