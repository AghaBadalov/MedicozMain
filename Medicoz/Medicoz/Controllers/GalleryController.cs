using Medicoz.DAL;
using Medicoz.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Medicoz.Controllers
{
    
    public class GalleryController : Controller
    {
        private readonly AppDbContext _context;

        public GalleryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Gallery> galleries = _context.Galleries.Include(x=>x.Department).ToList();
            return View(galleries);
        }

    }
}
