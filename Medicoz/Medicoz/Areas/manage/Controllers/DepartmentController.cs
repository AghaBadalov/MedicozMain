using Medicoz.DAL;
using Medicoz.Helpers;
using Medicoz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security;

namespace Medicoz.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]

    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DepartmentController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page=1)
        {
            var query = _context.Departments.AsQueryable();
            PaginatedList<Department> departments = PaginatedList<Department>.Create(query, 6, page);
            return View(departments);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if(!ModelState.IsValid) return View();
            if(department.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Can't be null");
                return View();
            }
            if (department.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "Image size must be 2mb or lower");
                return View();
            }
            if (department.ImageFile.ContentType != "image/png" && department.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Image type must be png, jpg or jpeg");
                return View();
            }
            department.Imageurl = department.ImageFile.SaveFile("uploads/departments", _env.WebRootPath);
            _context.Departments.Add(department); 
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            Department department = _context.Departments.FirstOrDefault(x=>x.Id==id);
            if(department==null) return View("Error");
            return View(department);
        }
        [HttpPost]
        public IActionResult Update(Department department)
        {
            Department exstdepartment=_context.Departments.FirstOrDefault(x=>x.Id == department.Id);
            if(exstdepartment==null) return View("Error");
            if (!ModelState.IsValid) return View();
            if(department.ImageFile != null)
            {
                if (department.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Image size must be 2mb or lower");
                    return View();
                }
                if (department.ImageFile.ContentType != "image/png" && department.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Image type must be png, jpg or jpeg");
                    return View();
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/departments", exstdepartment.Imageurl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                exstdepartment.Imageurl = department.ImageFile.SaveFile("uploads/departments", _env.WebRootPath);
            }
            exstdepartment.Name = department.Name;
            exstdepartment.Tittle = department.Tittle;
            exstdepartment.Description1 = department.Description1;
            exstdepartment.Description2 = department.Description2;
            exstdepartment.IconUrl=department.IconUrl;
            
            _context.SaveChanges();
            return RedirectToAction("index");

        }
        public IActionResult Delete(int id)
        {
            Department department = _context.Departments.FirstOrDefault(x => x.Id == id);
            if (department == null) return View("Error");
            string path = Path.Combine(_env.WebRootPath, "uploads/departments", department.Imageurl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Departments.Remove(department);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
