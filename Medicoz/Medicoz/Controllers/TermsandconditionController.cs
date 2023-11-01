using Medicoz.DAL;
using Medicoz.Models;
using Microsoft.AspNetCore.Mvc;

namespace Medicoz.Controllers
{
    public class TermsandconditionController : Controller
    {
        private readonly AppDbContext _context;

        public TermsandconditionController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<TermandCondition> terms = _context.TermandConditions.ToList();
            return View(terms);
        }
    }
}
