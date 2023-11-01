using Medicoz.DAL;
using Medicoz.Models;
using Medicoz.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medicoz.Controllers
{
    public class PlanController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
    }
}
