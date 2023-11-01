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

    public class FeatureController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public FeatureController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page=1)
        {
            //List<Feature> features=_context.Features.Where(x=>x.IsDeleted==false).ToList();
            var query = _context.Features.Where(x => x.IsDeleted == false).AsQueryable();
            PaginatedList<Feature> features = PaginatedList<Feature>.Create(query, 6,page);
            return View(features);
        }
        public IActionResult DeletedFeature(int page=1)
        {
            var query = _context.Features.Where(x => x.IsDeleted == true).AsQueryable();
            PaginatedList<Feature> features = PaginatedList<Feature>.Create(query, 6, page);
            return View(features);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Feature feature)
        {
            if (!ModelState.IsValid) return View();
            _context.Features.Add(feature);
            feature.IsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            Feature feature = _context.Features.FirstOrDefault(x => x.Id == id);
            if(feature == null) return View("error");
            return View(feature);
        }
        [HttpPost]
        public IActionResult Update(Feature feature)
        {
            Feature exstfeature = _context.Features.FirstOrDefault(x => x.Id == feature.Id);
            if(exstfeature is null) return View("error");
            if (!ModelState.IsValid) return View();
            exstfeature.Tittle= feature.Tittle;
            exstfeature.Desc= feature.Desc;
            exstfeature.Icon=feature.Icon;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult SoftDelete(int id)
        {
            Feature feature = _context.Features.FirstOrDefault(x => x.Id == id);
            if (feature == null) return View("error");
            feature.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("index");

        }
        public IActionResult Repair(int id)
        {
            Feature feature = _context.Features.FirstOrDefault(x => x.Id == id);
            if (feature == null) return View("error");
            feature.IsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction("deletedfeature");

        }
        public IActionResult Delete(int id)
        {
            Feature feature = _context.Features.FirstOrDefault(x => x.Id == id);
            if (feature == null) return View("error");
            _context.Features.Remove(feature);
            _context.SaveChanges();
            return RedirectToAction("deletedfeature");
        }
    }
}
