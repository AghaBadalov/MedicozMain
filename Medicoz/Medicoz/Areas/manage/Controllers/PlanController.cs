using Medicoz.DAL;
using Medicoz.Helpers;
using Medicoz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Medicoz.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]

    public class PlanController : Controller
    {
        private readonly AppDbContext _context;

        public PlanController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            //List<Plan> plans = _context.Plans.Include(x=>x.PlanCategory).Where(x=>x.IsDeleted==false).ToList();
            var query= _context.Plans.Include(x => x.PlanCategory).Where(x => x.IsDeleted == false).AsQueryable();
            PaginatedList<Plan> plans = PaginatedList<Plan>.Create(query, 6, page);

            return View(plans);
        }
        public IActionResult DeletedPlans(int page=1)
        {
            var query = _context.Plans.Include(x => x.PlanCategory).Where(x => x.IsDeleted == true).AsQueryable();
            PaginatedList<Plan> plans = PaginatedList<Plan>.Create(query, 6, page);

            return View(plans);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = _context.PlanCategories;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Plan plan)
        {
            ViewBag.Categories = _context.PlanCategories;

            if (!ModelState.IsValid) return View();
            if (_context.PlanCategories.FirstOrDefault() ==null)
            {
                ModelState.AddModelError("PlanCategoryId", "Required");
                return View();
            }
            _context.Plans.Add(plan);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            ViewBag.Categories = _context.PlanCategories;

            Plan plan = _context.Plans.FirstOrDefault(x=>x.Id==id);
            if (plan == null) return View("error");
            return View(plan);
        }
        [HttpPost]
        public IActionResult Update(Plan plan)
        {
            ViewBag.Categories = _context.PlanCategories;

            Plan exstplan =_context.Plans.FirstOrDefault(x=>x.Id==plan.Id);
            if(exstplan == null) return View("error");
            if (!ModelState.IsValid) return View(plan);
            exstplan.Planperiod = plan.Planperiod;
            exstplan.PlanCategoryId = plan.PlanCategoryId;
            exstplan.Feature1 = plan.Feature1;
            exstplan.Feature2 = plan.Feature2;
            exstplan.Feature3 = plan.Feature3;
            exstplan.Feature4 = plan.Feature4;
            
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Repair(int id)
        {
            Plan plan = _context.Plans.FirstOrDefault(x => x.Id == id);
            if (plan == null) return View("error");
            plan.IsDeleted= false;
            _context.SaveChanges();
            return RedirectToAction("deletedplans");

        }
        public IActionResult Softdelete(int id)
        {
            Plan plan = _context.Plans.FirstOrDefault(x => x.Id == id);
            if (plan == null) return View("error");
            plan.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Plan plan = _context.Plans.FirstOrDefault(x => x.Id == id);
            if (plan == null) return View("error");
            _context.Plans.Remove(plan);
            _context.SaveChanges();
            return RedirectToAction("deletedplans");

        }
    }
}
