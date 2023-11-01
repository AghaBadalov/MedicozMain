using Medicoz.DAL;
using Medicoz.Helpers;
using Medicoz.Models;
using Medicoz.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Medicoz.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(ShopVM shopVM,string filter,double? price,double? desentprice,int page=1)
        {
            //var query = _context.Products.AsQueryable();
            //PaginatedList<Product> products = PaginatedList<Product>.Create(query, 6, page);
            //List<Product> products = _context.Products.ToList();
            var products = _context.Products.AsQueryable();
            var query = products;
            switch (filter)
            {
                case "price":
                    query = products.OrderBy(x => x.SalePrice);
                    break;

                case "desent":
                    query = products.OrderByDescending(x => x.SalePrice);
                    break;
                case "Discount":
                    query = products.OrderByDescending(x => x.DiscountPrice);
                    break;

                default:
                    break;
            }

            ViewBag.filters = filter;
            shopVM = new ShopVM
            {
                Products = PaginatedList<Product>.Create(query, 6, page)
            };
            return View(shopVM);
        }
        public IActionResult Detail(int id)
        {
            Product product = _context.Products.FirstOrDefault(x=>x.Id==id);
            if (product == null) return NotFound();
            
            return View(product);
        }
    }
}
