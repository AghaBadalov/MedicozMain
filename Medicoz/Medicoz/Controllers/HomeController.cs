using Medicoz.DAL;
using Medicoz.Models;
using Medicoz.Services;
using Medicoz.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Medicoz.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
       

        public IActionResult Index()
        {
            HomeVM vm = new HomeVM
            {
                Sliders=_context.Sliders.Where(x=>x.IsDeleted==false).ToList(),
                Plans=_context.Plans.Where(x=>x.IsDeleted==false).Include(x=>x.PlanCategory).ToList(),
                Features=_context.Features.Where(x=>x.IsDeleted==false).ToList(),
                Ads=_context.Ads.Where(x=>x.Isdeleted==false).ToList(),
                Testimonials=_context.Testimonials.Where(x => x.IsDeleted == false).ToList(),
                Doctorcount=_context.Doctors.Count(),
                Departmentcount=_context.Departments.Count(),
                MemberCount=_context.Users.Count(),
                ReservationCount=_context.Appointments.Count()
            };
            return View(vm);
        }
        
        
    }
}