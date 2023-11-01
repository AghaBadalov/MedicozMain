using Medicoz.DAL;
using Medicoz.Models;
using Medicoz.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Medicoz.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly AppDbContext _context;

        public DepartmentController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Department> Departments = _context.Departments.ToList();
            return View(Departments);
        }
        public IActionResult Detail(int id)
        {
            DepartmentVm departmentVm = new DepartmentVm
            {
                Department = _context.Departments.FirstOrDefault(x => x.Id == id),
                Departments = _context.Departments.ToList()

            };
            if (departmentVm.Department == null) return NotFound();
            return View(departmentVm);
        }
    }
}
