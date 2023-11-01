using Medicoz.DAL;
using Medicoz.Models;
using Medicoz.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Medicoz.Controllers
{
    public class AboutController : Controller
    {
        private readonly AppDbContext _context;

        public AboutController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            AboutpageVM vm = new AboutpageVM
            {
                About = _context.Abouts.FirstOrDefault(),
                Testimonials = _context.Testimonials.Where(x => x.IsDeleted == false).ToList(),
                Ads = _context.Ads.Where(x => x.Isdeleted == false).ToList(),
                Doctors = _context.Doctors.Where(x => x.IsDeleted == false).ToList(),
                DepartmentCount = _context.Departments.Count(),
                DoctorCount = _context.Doctors.Count(),
                AppointmentCount = _context.Appointments.Count(),
                UserCount = _context.Users.Count(),
            };
            
            return View(vm);
        }
    }
}
