using Medicoz.DAL;
using Medicoz.Helpers;
using Medicoz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Medicoz.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]

    public class TermsandconditionsController : Controller
    {
        private readonly AppDbContext _context;

        public TermsandconditionsController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            var query = _context.TermandConditions.Where(x => x.IsDeleted == false).AsQueryable();
            PaginatedList<TermandCondition> terms = PaginatedList<TermandCondition>.Create(query, 6, page);
            return View(terms);
        }
        public IActionResult DeletedTerms(int page=1)
        {
            var query = _context.TermandConditions.Where(x => x.IsDeleted == true).AsQueryable();
            PaginatedList<TermandCondition> terms = PaginatedList<TermandCondition>.Create(query, 6, page);
            return View(terms);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TermandCondition term)
        {
            if (!ModelState.IsValid) return View();
            _context.TermandConditions.Add(term);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            TermandCondition term = _context.TermandConditions.FirstOrDefault(x => x.Id == id);
            if(term == null) return View("error");
            return View(term);
        }
        [HttpPost]
        public IActionResult Update(TermandCondition term)
        {
            TermandCondition exstterm = _context.TermandConditions.FirstOrDefault(x => x.Id == term.Id);
            if (exstterm is null) return View("error");
            if (!ModelState.IsValid) return View(term);
            exstterm.Desc = term.Desc;
            exstterm.Name = term.Name;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Repair(int id)
        {
            TermandCondition term = _context.TermandConditions.FirstOrDefault(x => x.Id == id);
            if (term == null) return View("error");
            term.IsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction("deletedterms");
        }
        public IActionResult Softdelete(int id)
        {
            TermandCondition term = _context.TermandConditions.FirstOrDefault(x => x.Id == id);
            if (term == null) return View("error");
            term.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            TermandCondition term = _context.TermandConditions.FirstOrDefault(x => x.Id == id);
            if (term == null) return View("error");
            _context.TermandConditions.Remove(term);
            _context.SaveChanges();
            return RedirectToAction("index");
        }



    }
}
