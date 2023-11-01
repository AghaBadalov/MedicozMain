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

    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        

        public ServiceController(AppDbContext context)
        {
            _context = context;
            
        }
        public IActionResult Index(int page=1)
        {

            var query= _context.Services.Where(x => x.IsDeleted == false).AsQueryable();
            PaginatedList<Service> services = PaginatedList<Service>.Create(query, 6, page);
            return View(services);
        }
        public IActionResult DeletedFeature()
        {
            List<Service> services = _context.Services.Where(x => x.IsDeleted == true).ToList();
            return View(services);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Service service)
        {
            if (!ModelState.IsValid) return View();
            _context.Services.Add(service);
            service.IsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            Service service = _context.Services.FirstOrDefault(x => x.Id == id);
            if (service == null) return View("error");
            return View(service);
        }
        [HttpPost]
        public IActionResult Update(Service service)
        {
            Service exstservice = _context.Services.FirstOrDefault(x => x.Id == service.Id);
            if (exstservice is null) return View("error");
            if (!ModelState.IsValid) return View(service);
            exstservice.Tittle = service.Tittle;
            exstservice.Desc = service.Desc;
            exstservice.Icon = service.Icon;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult SoftDelete(int id)
        {
            Service service = _context.Services.FirstOrDefault(x => x.Id == id);
            if (service == null) return View("error");
            service.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("index");

        }
        public IActionResult Repair(int id)
        {
            Service service = _context.Services.FirstOrDefault(x => x.Id == id);
            if (service == null) return View("error");
            service.IsDeleted = false;
            _context.SaveChanges();
            return RedirectToAction("deletedservice");

        }
        public IActionResult Delete(int id)
        {
            Service service = _context.Services.FirstOrDefault(x => x.Id == id);
            if (service == null) return View("error");
            _context.Services.Remove(service);
            _context.SaveChanges();
            return RedirectToAction("deletedservice");
        }
    }
}
