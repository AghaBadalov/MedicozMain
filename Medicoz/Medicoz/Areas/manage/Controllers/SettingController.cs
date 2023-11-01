using Medicoz.DAL;
using Medicoz.Helpers;
using Medicoz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Configuration;
using System.Data;
using Settings = Medicoz.Models.Settings;

namespace Medicoz.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]

    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SettingController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page=1)
        {
            
            var query = _context.Settings.AsQueryable();
            PaginatedList<Settings> settings = PaginatedList<Settings>.Create(query, 6, page);
            return View(settings);
        }
        public IActionResult Update(int id)
        {
            Settings settings = _context.Settings.FirstOrDefault(x => x.Id == id);
            if (settings == null) return View("error");
            return View(settings);
        }
        [HttpPost]
        public IActionResult Update(Settings settings)
        {
            Settings exstsettings = _context.Settings.FirstOrDefault(x => x.Id == settings.Id);
            if(exstsettings == null) return View("error");
            if(settings.Value is null)
            {
                ModelState.AddModelError("Value", "Value can't be null");
                return View(settings);
            }
            if (!ModelState.IsValid) return View(settings);
            exstsettings.Value = settings.Value;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult ImageUpdate(int id)
        {
            Settings settings = _context.Settings.FirstOrDefault(x => x.Id == id);
            if (settings == null) return View("error");
            return View(settings);
        }
        [HttpPost]
        public IActionResult ImageUpdate(Settings setting){
            Settings exstsettings = _context.Settings.FirstOrDefault(x => x.Id == setting.Id);
            if (exstsettings == null) return View("error");
            if (!ModelState.IsValid) return View(setting);
            
            if (setting.ImageFile != null)
            {
                if (setting.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Image size must be 2mb or lower");
                    return View(setting);
                }
                if (setting.ImageFile.ContentType != "image/png" && setting.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Image type must be png, jpg or jpeg");
                    return View(setting);
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/settings", exstsettings.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                exstsettings.ImageUrl = setting.ImageFile.SaveFile("uploads/settings", _env.WebRootPath);
            }
            exstsettings.Value = exstsettings.ImageUrl;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
