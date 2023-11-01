using Medicoz.DAL;
using Microsoft.AspNetCore.Mvc;

namespace Medicoz.Areas.manage.Controllers
{
    [Area("manage")]
    public class ContactUsController : Controller
    {
        public ContactUsController(AppDbContext context)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Update(int id)
        {
            return View();
        }
    }
}
