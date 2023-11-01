using Medicoz.DAL;
using Medicoz.Models;
using Microsoft.AspNetCore.Mvc;

namespace Medicoz.ViewComponents
{
    public class BlogViewComponent :ViewComponent
    {
        private readonly AppDbContext _context;

        public BlogViewComponent(AppDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            List<BlogPost> posts = _context.BlogPosts.ToList();
            return View(posts);
        }
    }
}
