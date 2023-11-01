using Medicoz.DAL;
using Medicoz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medicoz.ViewComponents
{
    public class PricingViewComponent :ViewComponent
    {
        private readonly AppDbContext _context;

        public PricingViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            List<Plan> plans=_context.Plans.Where(x=>x.IsDeleted==false).Include(x=>x.PlanCategory).ToList();
            return View(plans);
        }
    }
}
