using Medicoz.DAL;
using Medicoz.Helpers;
using Medicoz.Migrations;
using Medicoz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.Data;

namespace Medicoz.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]

    public class TestimonialController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TestimonialController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page=1)
        {
            var query=_context.Testimonials.Where(x=>x.IsDeleted==false).AsQueryable();
            PaginatedList<Testimonial> testimonials = PaginatedList<Testimonial>.Create(query, 5, page);
            return View(testimonials);
        }
        public IActionResult DeletedTestimonials(int page = 1)
        {
            var query = _context.Testimonials.Where(x => x.IsDeleted == true).AsQueryable();
            PaginatedList<Testimonial> testimonials = PaginatedList<Testimonial>.Create(query, 5, page);
            return View(testimonials);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Testimonial newTestimonial)
        {
            if (!ModelState.IsValid) return View(newTestimonial);
            if(newTestimonial.ImageFIle is null)
            {
                ModelState.AddModelError("ImageFIle", "Can't be null");
                return View(newTestimonial);
            }
            if (newTestimonial.ImageFIle.Length > 2097152)
            {
                ModelState.AddModelError("ImageFIle", "Image size must be 2mb or lower");
                return View();
            }
            if (newTestimonial.ImageFIle.ContentType != "image/png" && newTestimonial.ImageFIle.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFIle", "Image type must be png, jpg or jpeg");
                return View();
            }
            newTestimonial.ImageUrl = newTestimonial.ImageFIle.SaveFile("uploads/testimonials", _env.WebRootPath);
            _context.Testimonials.Add(newTestimonial);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            Testimonial testimonial = _context.Testimonials.FirstOrDefault(x => x.Id == id);
            if (testimonial is null) return NotFound();
            return View(testimonial);

        }
        [HttpPost]
        public IActionResult Update(Testimonial testimonial)
        {
            Testimonial exsttestimonial = _context.Testimonials.FirstOrDefault(x => x.Id == testimonial.Id);
            if (exsttestimonial is null) return NotFound();
            if (!ModelState.IsValid) return View(testimonial);
            if(testimonial.ImageFIle != null)
            {
                if (testimonial.ImageFIle.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFIle", "Image size must be 2mb or lower");
                    return View();
                }
                if (testimonial.ImageFIle.ContentType != "image/png" && testimonial.ImageFIle.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFIle", "Image type must be png, jpg or jpeg");
                    return View();
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/testimonials", exsttestimonial.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                exsttestimonial.ImageUrl = testimonial.ImageFIle.SaveFile("uploads/testimonials", _env.WebRootPath);
            }
            exsttestimonial.Position = testimonial.Position;
            exsttestimonial.Name = testimonial.Name;
            exsttestimonial.Desc = testimonial.Desc;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult SoftDelete(int id)
        {
            Testimonial testimonial = _context.Testimonials.FirstOrDefault(x => x.Id == id);
            if (testimonial is null) return NotFound();
            testimonial.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Repair(int id)
        {
            Testimonial testimonial = _context.Testimonials.FirstOrDefault(x => x.Id == id);
            if (testimonial is null) return NotFound();
            testimonial.IsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction("deletedtestimonials");
        }
        public IActionResult Delete(int id)
        {
            Testimonial testimonial = _context.Testimonials.FirstOrDefault(x => x.Id == id);
            if (testimonial is null) return NotFound();
            string path = Path.Combine(_env.WebRootPath, "uploads/testimonials",testimonial.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Testimonials.Remove(testimonial);
            _context.SaveChanges();
            return RedirectToAction("deletedtestimonials");
        }
    }
}
