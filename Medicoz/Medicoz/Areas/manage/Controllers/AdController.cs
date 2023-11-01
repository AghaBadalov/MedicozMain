using Medicoz.DAL;
using Medicoz.Helpers;
using Medicoz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Medicoz.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]


    public class AdController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AdController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page=1)
        {
            //List<Ad> ads = _context.Ads.Where(x=>x.Isdeleted==false).ToList();
            var query=_context.Ads.Where(x=>x.Isdeleted==false).AsQueryable();
            PaginatedList<Ad> ads = PaginatedList<Ad>.Create(query, 6, page);
            return View(ads);
        }
        public IActionResult DeletedAd(int page=1)
        {
            //List<Ad> ads = _context.Ads.Where(x => x.Isdeleted == true).ToList();
            var query = _context.Ads.Where(x => x.Isdeleted == true).AsQueryable();
            PaginatedList<Ad> ads=PaginatedList<Ad>.Create(query, 6, page);

            return View(ads);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Ad ad)
        {
            if (!ModelState.IsValid) return View();
            if(ad.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Can't be null");
                return View();
            }

            if(ad.ImageFile.Length> 2097152)
            {
                ModelState.AddModelError("ImageFile", "Image size must be 2mb or less");
                return View();
            }
            if (ad.ImageFile.ContentType != "image/png" && ad.ImageFile.ContentType != "image/jpeg")
            {



                ModelState.AddModelError("ImageFile", "File must be jpeg, jpg or png");
                return View();
            }
            ad.ImageUrl = ad.ImageFile.SaveFile("uploads/ad", _env.WebRootPath);
            _context.Ads.Add(ad);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            Ad ad = _context.Ads.FirstOrDefault(x => x.Id == id);
            if (ad == null) return View("error");
            return View(ad);
        }
        [HttpPost]
        public IActionResult Update(Ad ad)
        {
            Ad exstad=_context.Ads.FirstOrDefault(x=>x.Id==ad.Id);
            if (exstad == null) return View("error");
            if (!ModelState.IsValid) return View(ad);
            if(ad.ImageFile!= null)
            {
                if (ad.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Image size must be 2mb or less");
                    return View(ad);
                }
                if (ad.ImageFile.ContentType != "image/png" && ad.ImageFile.ContentType != "image/jpeg")
                {



                    ModelState.AddModelError("ImageFile", "File must be jpeg, jpg or png");
                    return View(ad);
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/ad", exstad.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                exstad.ImageUrl=ad.ImageFile.SaveFile("uploads/ad",_env.WebRootPath);
            }
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Repair(int id)
        {
            Ad ad = _context.Ads.FirstOrDefault(x => x.Id == id);
            if (ad == null) return View("error");
            ad.Isdeleted = false;
            _context.SaveChanges();
            return RedirectToAction("deletedad");
        }
        public IActionResult Softdelete(int id)
        {
            Ad ad = _context.Ads.FirstOrDefault(x => x.Id == id);
            if (ad == null) return View("error");
            ad.Isdeleted = true;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Ad ad = _context.Ads.FirstOrDefault(x => x.Id == id);
            if (ad == null) return View("error");
            string path = Path.Combine(_env.WebRootPath, "uploads/ad", ad.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Ads.Remove(ad);
            _context.SaveChanges();

            return RedirectToAction("deletedad");
        }
    }
}
