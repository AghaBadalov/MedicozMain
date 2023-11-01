using Medicoz.DAL;
using Medicoz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medicoz.ViewComponents
{
    public class DoctorViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public DoctorViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            List<Doctor> doctors = _context.Doctors.Where(x=>x.IsDeleted==false).Include(x=>x.Department).ToList();
            return View(doctors);
        }
    }
}
