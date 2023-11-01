using Medicoz.DAL;
using Medicoz.Helpers;
using Medicoz.Models;
using Microsoft.AspNetCore.Mvc;

namespace Medicoz.Areas.manage.Controllers
{
    [Area("manage")]
    public class FunFactController : Controller
    {
        private readonly AppDbContext _context;

        public FunFactController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            var query=_context.FunFacts.AsQueryable();
            PaginatedList<FunFact> funFacts = PaginatedList<FunFact>.Create(query, 6, page);
            return View(funFacts);
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
