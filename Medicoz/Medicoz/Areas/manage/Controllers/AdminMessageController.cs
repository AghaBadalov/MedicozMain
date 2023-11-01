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

    public class AdminMessageController : Controller
    {
        private readonly AppDbContext _context;

        public AdminMessageController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            var query = _context.Messages.AsQueryable();
            PaginatedList<AdminMessage> messages = PaginatedList<AdminMessage>.Create(query, 6, page);
            return View(messages);
        }
    }
}
