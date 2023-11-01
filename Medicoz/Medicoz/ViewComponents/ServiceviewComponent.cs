using Medicoz.DAL;
using Medicoz.Models;
using Microsoft.AspNetCore.Mvc;

namespace Medicoz.ViewComponents
{
    public class ServiceviewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public ServiceviewComponent(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            List<Service> services = _context.Services.Where(x=>x.IsDeleted==false).ToList();
            return View(services);
        }
    }
}
