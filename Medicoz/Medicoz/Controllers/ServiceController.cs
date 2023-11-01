using Microsoft.AspNetCore.Mvc;

namespace Medicoz.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
