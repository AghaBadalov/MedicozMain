using Medicoz.DAL;
using Medicoz.Helpers;
using Medicoz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Data;

namespace Medicoz.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]
    

    public class GalleryController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public GalleryController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page=1)
        {
            var query = _context.Galleries.Include(x=>x.Department).AsQueryable();
            PaginatedList<Gallery> galleries = PaginatedList<Gallery>.Create(query, 6, page);
            return View(galleries);
        }
        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Gallery gallery)
        {
            ViewBag.Departments = _context.Departments;
            if (!ModelState.IsValid) return View(gallery);
            if(gallery.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Can't be null");
                return View(gallery);
            }
            if (gallery.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "Image size must be 2mb or less");
                return View(gallery);
            }
            if (gallery.ImageFile.ContentType != "image/png" && gallery.ImageFile.ContentType != "image/jpeg")
            {



                ModelState.AddModelError("ImageFile", "File must be jpeg, jpg or png");
                return View();
            }
            gallery.Imageurl = gallery.ImageFile.SaveFile("uploads/galleries", _env.WebRootPath);
            _context.Galleries.Add(gallery);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            ViewBag.Departments = _context.Departments;

            Gallery gallery = _context.Galleries.FirstOrDefault(x => x.Id == id);
            if(gallery == null) return NotFound();
            return View(gallery);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Gallery gallery)
        {
            ViewBag.Departments = _context.Departments;

            Gallery exstgallery = _context.Galleries.FirstOrDefault(x => x.Id == gallery.Id);
            if (exstgallery is null) return NotFound();
            if (!ModelState.IsValid) return View(gallery);
            if(gallery.ImageFile!= null)
            {
                if (gallery.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Image size must be 2mb or less");
                    return View(gallery);
                }
                if (gallery.ImageFile.ContentType != "image/png" && gallery.ImageFile.ContentType != "image/jpeg")
                {



                    ModelState.AddModelError("ImageFile", "File must be jpeg, jpg or png");
                    return View();
                }
                string path=Path.Combine(_env.WebRootPath,"uploads/galleries",exstgallery.Imageurl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                exstgallery.Imageurl = gallery.ImageFile.SaveFile("uploads/galleries", _env.WebRootPath);
            }
            exstgallery.DepartmentId = gallery.DepartmentId;
            exstgallery.Tittle=gallery.Tittle;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Gallery gallery = _context.Galleries.FirstOrDefault(x => x.Id == id);
            if (gallery == null) return NotFound();
            string path = Path.Combine(_env.WebRootPath, "uploads/galleries", gallery.Imageurl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Galleries.Remove(gallery);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

    }
}
