using Medicoz.DAL;
using Medicoz.Helpers;
using Medicoz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Numerics;

namespace Medicoz.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]

    public class AboutController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AboutController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<About> abouts = _context.Abouts.ToList();
            
            return View(abouts);
        }
        public IActionResult Create()
        {
            if (_context.Abouts.Count() > 0) return NotFound();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(About about)
        {
           
            if (!ModelState.IsValid) return View();
            if(about.MiddleImage is null)
            {
                ModelState.AddModelError("MiddleImage", "Can't be null");
                return View(about);
            }
            if (about.BigImage is null)
            {
                ModelState.AddModelError("BigImage", "Can't be null");
                return View(about);
            }
            if (about.SmallImage is null)
            {
                ModelState.AddModelError("SmallImage", "Can't be null");
                return View(about);
            }
            //middle
            if (about.MiddleImage.Length > 2097152)
            {
                ModelState.AddModelError("MiddleImage", "Image size must be 2mb or lower");
                return View();
            }
            if (about.MiddleImage.ContentType != "image/png" && about.MiddleImage.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("MiddleImage", "Image type must be png, jpg or jpeg");
                return View();
            }
            about.MiddleImageUrl = about.MiddleImage.SaveFile("uploads/abouts", _env.WebRootPath);
            //Small
            if (about.SmallImage.Length > 2097152)
            {
                ModelState.AddModelError("SmallImage", "Image size must be 2mb or lower");
                return View();
            }
            if (about.SmallImage.ContentType != "image/png" && about.SmallImage.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("SmallImage", "Image type must be png, jpg or jpeg");
                return View();
            }
            about.SmallImageUrl = about.SmallImage.SaveFile("uploads/abouts", _env.WebRootPath);
            //Big
            if (about.BigImage.Length > 2097152)
            {
                ModelState.AddModelError("BigImage", "Image size must be 2mb or lower");
                return View();
            }
            if (about.BigImage.ContentType != "image/png" && about.BigImage.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("BigImage", "Image type must be png, jpg or jpeg");
                return View();
            }
            about.BigImageUrl = about.BigImage.SaveFile("uploads/abouts", _env.WebRootPath);
            _context.Abouts.Add(about);
            _context.SaveChanges();
            return RedirectToAction("index");

        }
        public IActionResult Update(int id)
        {
            About about = _context.Abouts.FirstOrDefault(x => x.Id == id);
            if(about == null) return View("error");
            return View(about);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(About about)
        {
            About exstabout=_context.Abouts.FirstOrDefault(x=>x.Id == about.Id);
            if (exstabout == null) return View("error");
            if (!ModelState.IsValid) return View(about);
            if(about.MiddleImage != null)
            {
                if (about.MiddleImage.Length > 2097152)
                {
                    ModelState.AddModelError("MiddleImage", "Image size must be 2mb or lower");
                    return View(about);
                }
                if (about.MiddleImage.ContentType != "image/png" && about.MiddleImage.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("MiddleImage", "Image type must be png, jpg or jpeg");
                    return View(about);
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/abouts", exstabout.MiddleImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                exstabout.MiddleImageUrl = about.MiddleImage.SaveFile("uploads/abouts", _env.WebRootPath);

            }
            if (about.SmallImage != null)
            {
                if (about.SmallImage.Length > 2097152)
                {
                    ModelState.AddModelError("SmallImage", "Image size must be 2mb or lower");
                    return View(about);
                }
                if (about.SmallImage.ContentType != "image/png" && about.SmallImage.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("SmallImage", "Image type must be png, jpg or jpeg");
                    return View(about);
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/abouts", exstabout.SmallImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                exstabout.SmallImageUrl = about.SmallImage.SaveFile("uploads/abouts", _env.WebRootPath);

            }
            if (about.BigImage != null)
            {
                if (about.BigImage.Length > 2097152)
                {
                    ModelState.AddModelError("BigImage", "Image size must be 2mb or lower");
                    return View(about);
                }
                if (about.BigImage.ContentType != "image/png" && about.BigImage.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("BigImage", "Image type must be png, jpg or jpeg");
                    return View(about);
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/abouts", exstabout.BigImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                exstabout.BigImageUrl = about.BigImage.SaveFile("uploads/abouts", _env.WebRootPath);

            }
            exstabout.Tittle=about.Tittle;
            exstabout.Desc = about.Desc;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            About about = _context.Abouts.FirstOrDefault(x => x.Id == id);
            if (about == null) return View("error");
            string path1 = Path.Combine(_env.WebRootPath, "uploads/abouts", about.SmallImageUrl);
            if (System.IO.File.Exists(path1))
            {
                System.IO.File.Delete(path1);
            }
            string path2 = Path.Combine(_env.WebRootPath, "uploads/abouts", about.MiddleImageUrl);
            if (System.IO.File.Exists(path2))
            {
                System.IO.File.Delete(path2);
            }
            string path3 = Path.Combine(_env.WebRootPath, "uploads/abouts", about.BigImageUrl);
            if (System.IO.File.Exists(path3))
            {
                System.IO.File.Delete(path3);
            }
            _context.Abouts.Remove(about);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
