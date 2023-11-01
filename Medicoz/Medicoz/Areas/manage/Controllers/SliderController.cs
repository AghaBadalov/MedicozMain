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

    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page=1)
        {
            var query = _context.Sliders.Where(x => x.IsDeleted == false).AsQueryable();
            PaginatedList<Slider> sliders = PaginatedList<Slider>.Create(query, 6, page);
            return View(sliders);
        }
        public IActionResult DeletedSliders(int page=1)
        {
            var query = _context.Sliders.Where(x => x.IsDeleted == true).AsQueryable();
            PaginatedList<Slider> sliders = PaginatedList<Slider>.Create(query, 6, page);
            return View(sliders);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if (!ModelState.IsValid) return View(slider);
            if (slider.ImageFile == null) {
                ModelState.AddModelError("ImageFile", "Cant'be null");
                return View();
            }
            if (slider.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "Image size must be 2mb or lower");
                return View();
            }
            if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Image type must be png, jpg or jpeg");
                return View();
            }
            slider.ImageUrl = slider.ImageFile.SaveFile("uploads/sliders", _env.WebRootPath);
            slider.IsDeleted = false;
            _context.Sliders.Add(slider);
            _context.SaveChanges();



            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id);
            if (slider == null) return View("error");


            return View(slider);
        }
        [HttpPost]
        public IActionResult Update(Slider slider)
        {
            Slider exstsilder = _context.Sliders.FirstOrDefault(x => x.Id == slider.Id);
            if (exstsilder == null) return View("error");
            if (!ModelState.IsValid) return View(slider);
            if (slider.ImageFile != null)
            {
                if (slider.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Image size must be 2mb or lower");
                    return View(slider);
                }
                if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Image type must be png, jpg or jpeg");
                    return View(slider);
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/sliders", exstsilder.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                exstsilder.ImageUrl = slider.ImageFile.SaveFile("uploads/sliders", _env.WebRootPath);
            }
            exstsilder.Desc = slider.Desc;
            
            exstsilder.Tittle = slider.Tittle;
            exstsilder.Tittle2 = slider.Tittle2;
            _context.SaveChanges();



            return RedirectToAction("index");
        }
        public IActionResult HardDelete(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id);
            if (slider == null) return View("error");
            string path = Path.Combine(_env.WebRootPath, "uploads/sliders", slider.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Sliders.Remove(slider);
            _context.SaveChanges();
            return RedirectToAction("deletedsliders");
        }
        public IActionResult Delete(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id);
            if (slider == null) return View("error");
            slider.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Repair(int id)
        {
            Slider slider = _context.Sliders.FirstOrDefault(x => x.Id == id);
            if (slider == null) return View("error");
            slider.IsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
