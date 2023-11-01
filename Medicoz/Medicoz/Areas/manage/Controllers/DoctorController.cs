using Medicoz.DAL;
using Medicoz.Helpers;
using Medicoz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Medicoz.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]

    public class DoctorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DoctorController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page=1)
        {
            var query = _context.Doctors.Include(x=>x.Department).Where(x=>x.IsDeleted==false).AsQueryable();
            PaginatedList<Doctor> doctors = PaginatedList<Doctor>.Create(query, 6, page);

            return View(doctors);
        }
        public IActionResult DeletedDoctors(int page = 1)
        {
            var query = _context.Doctors.Include(x => x.Department).Where(x => x.IsDeleted == true).AsQueryable();
            PaginatedList<Doctor> doctors = PaginatedList<Doctor>.Create(query, 6, page);

            return View(doctors);
        }
        public IActionResult Create()
        {
            ViewBag.Departments = _context.Departments;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Doctor doctor)
        {
            ViewBag.Departments = _context.Departments;
            if (!ModelState.IsValid) return View(doctor);
            if (_context.Departments.FirstOrDefault() == null)
            {
                ModelState.AddModelError("DepartmentId", "Add Department");
                return View();
            }
            if (doctor.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Can't be null");
                return View();
            }
            if (doctor.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "Image size must be 2mb or lower");
                return View();
            }
            if (doctor.ImageFile.ContentType != "image/png" && doctor.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Image type must be png, jpg or jpeg");
                return View();
            }
            doctor.ImageUrl = doctor.ImageFile.SaveFile("uploads/doctors", _env.WebRootPath);
            
            
            if (doctor.WorkEndTime < doctor.WorkStartTime)
            {
                int time = (doctor.WorkStartTime.Hour - doctor.WorkEndTime.Hour);
                if (time < 12)
                {
                    ModelState.AddModelError("WorkEndTime", "WorkTime must be 12 or lower");
                    return View(doctor);
                }
            }
            else if(doctor.WorkEndTime > doctor.WorkStartTime)
            {
                int time = (doctor.WorkEndTime.Hour - doctor.WorkStartTime.Hour);
                if (time > 12 || time<1)
                {
                    ModelState.AddModelError("WorkEndTime", "WorkTime must be 12 or lower");
                    return View(doctor);
                }
            }
            else
            {
                ModelState.AddModelError("WorkEndTime", "Select else hour");
                return View();
            }
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            ViewBag.Departments = _context.Departments;

            Doctor doctor =_context.Doctors.FirstOrDefault(x => x.Id == id);
            if(doctor is null) return View("Error");
            return View(doctor);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Doctor doctor)
        {
            ViewBag.Departments = _context.Departments;

            Doctor exstdoctor =_context.Doctors.FirstOrDefault(x => x.Id == doctor.Id);
            if(exstdoctor is null) return View("Error");
            if (!ModelState.IsValid) return View(doctor);
            if(doctor.ImageFile != null)
            {
                if (doctor.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Image size must be 2mb or lower");
                    return View(doctor);
                }
                if (doctor.ImageFile.ContentType != "image/png" && doctor.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Image type must be png, jpg or jpeg");
                    return View(doctor);
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/doctors", exstdoctor.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                exstdoctor.ImageUrl = doctor.ImageFile.SaveFile("uploads/doctors",_env.WebRootPath);

            }
            if (doctor.WorkEndTime < doctor.WorkStartTime)
            {
                int time = (doctor.WorkStartTime.Hour - doctor.WorkEndTime.Hour);
                if (time < 12)
                {
                    ModelState.AddModelError("WorkEndTime", "WorkTime must be 12 or lower");
                    return View(doctor);
                }
            }
            else if (doctor.WorkEndTime > doctor.WorkStartTime)
            {
                int time = (doctor.WorkEndTime.Hour - doctor.WorkStartTime.Hour);
                if (time > 12 || time < 1)
                {
                    ModelState.AddModelError("WorkEndTime", "WorkTime must be 12 or lower");
                    return View(doctor);
                }
            }
            else
            {
                ModelState.AddModelError("WorkEndTime", "Select else hour");
                return View();
            }
            
            exstdoctor.Desc = doctor.Desc;
            exstdoctor.Name = doctor.Name;
            exstdoctor.DepartmentId = doctor.DepartmentId;
            exstdoctor.Education = doctor.Education;
            exstdoctor.Adress = doctor.Adress;
            exstdoctor.Phone = doctor.Phone;
            exstdoctor.Email = doctor.Email;
            exstdoctor.WorkStartTime = doctor.WorkStartTime;
            exstdoctor.WorkEndTime = doctor.WorkEndTime;
            exstdoctor.Website = doctor.Website;
            exstdoctor.Experience = doctor.Experience;
            exstdoctor.FBUrl = doctor.FBUrl;
            exstdoctor.TTUrl = doctor.TTUrl;
            exstdoctor.IGUrl = doctor.IGUrl;
            exstdoctor.LNUrl = doctor.LNUrl;
            _context.SaveChanges();
            return RedirectToAction("index");

        }
        public IActionResult Repair(int id)
        {
            Doctor doctor = _context.Doctors.FirstOrDefault(doctor => doctor.Id == id);
            if (doctor is null) return View("Error");
            doctor.IsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction("deleteddoctors");
        }
        public IActionResult SoftDelete(int id)
        {
            Doctor doctor = _context.Doctors.FirstOrDefault(doctor => doctor.Id == id);
            if (doctor is null) return View("Error");
            doctor.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Doctor doctor = _context.Doctors.FirstOrDefault(doctor => doctor.Id == id);
            if (doctor is null) return View("Error");
            string path = Path.Combine(_env.WebRootPath, "uploads/doctors", doctor.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Doctors.Remove(doctor);
            _context.SaveChanges();
            return RedirectToAction("deleteddoctors");
        }


    }
}
