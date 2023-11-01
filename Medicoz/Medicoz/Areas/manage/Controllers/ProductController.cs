using Medicoz.DAL;
using Medicoz.Helpers;
using Medicoz.Models;
using Medicoz.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Medicoz.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]

    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page=1)
        {
            var query=_context.Products.AsQueryable();
            PaginatedList<Product> products = PaginatedList<Product>.Create(query, 6, page);
            return View(products);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (!ModelState.IsValid) return View(product);
            if(product.ImageFile is null)
            {
                ModelState.AddModelError("ImageFIle", "Can't be null");
                return View(product);
            }
            if (product.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "Image size must be 2mb or lower");
                return View(product);
            }
            if (product.ImageFile.ContentType != "image/png" && product.ImageFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFile", "Image type must be png, jpg or jpeg");
                return View(product);
            }
            product.Status = Enums.Shopstatus.Sale;
            product.ImageUrl = product.ImageFile.SaveFile("uploads/products", _env.WebRootPath);
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Update(int id)
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null) return NotFound();
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Product product)
        {
            Product exstproduct = _context.Products.FirstOrDefault(x => x.Id == product.Id);
            if(exstproduct is null) return NotFound();
            if (!ModelState.IsValid) return View(product);
            if(product.ImageFile != null)
            {
                if (product.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Image size must be 2mb or lower");
                    return View(product);
                }
                if (product.ImageFile.ContentType != "image/png" && product.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "Image type must be png, jpg or jpeg");
                    return View(product);
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/products", exstproduct.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                exstproduct.ImageUrl=product.ImageFile.SaveFile("uploads/products",_env.WebRootPath);

            }
            exstproduct.Status = product.Status;
            exstproduct.Desc = product.Desc;
            exstproduct.Name = product.Name;
            exstproduct.SalePrice = product.SalePrice;
            exstproduct.DiscountPrice = product.DiscountPrice;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Delete(int id)
        {
            Product product = _context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null) return NotFound();
            string path = Path.Combine(_env.WebRootPath, "uploads/products", product.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
