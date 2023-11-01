using Medicoz.DAL;
using Medicoz.Models;
using Microsoft.AspNetCore.Mvc;

namespace Medicoz.ViewComponents
{
    public class AdViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public AdViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            List<Ad> ad=_context.Ads.Where(x=>x.Isdeleted==false).ToList();
            return View(ad);

        }
    }
}
