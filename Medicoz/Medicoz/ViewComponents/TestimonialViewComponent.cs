using Medicoz.DAL;
using Medicoz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medicoz.ViewComponents
{
    public class TestimonialViewComponent :ViewComponent
    {
        private readonly AppDbContext _context;

        public TestimonialViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            List<Testimonial> testimonials = _context.Testimonials.Where(x=>x.IsDeleted==false).ToList();

            return View(testimonials);
        }
    }
}
