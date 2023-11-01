using Medicoz.DAL;
using Medicoz.Models;
using Microsoft.AspNetCore.Mvc;

namespace Medicoz.ViewComponents
{
    public class AboutViewComponent :ViewComponent
    {
        private readonly AppDbContext _context;

        public AboutViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            List<About> abouts = _context.Abouts.ToList();
            return View(abouts);
        }
    }
}
