using Medicoz.DAL;
using Medicoz.Helpers;
using Medicoz.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Medicoz.Areas.manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = ("SuperAdmin,Admin"))]

    public class BlogPostController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogPostController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page=1)
        {
            var query = _context.BlogPosts.AsQueryable();
            PaginatedList<BlogPost> posts = PaginatedList<BlogPost>.Create(query, 5, page);
            return View(posts);
        }
        public IActionResult Detail(int id)
        {
            BlogPost post = _context.BlogPosts.FirstOrDefault(x => x.Id == id);
            if (post == null) return NotFound();
            
            return View(post);
        }
        public IActionResult Comments(int id,int page=1)
        {
            BlogPost post = _context.BlogPosts.Include(x=>x.Comments).FirstOrDefault(x => x.Id == id);
            if (post == null) return NotFound();
            var query = _context.BlogComments.Include(x => x.BlogPost).Where(x => x.BlogPostId == post.Id).AsQueryable();
            PaginatedList<BlogComment> comments = PaginatedList<BlogComment>.Create(query, 5, page);
            return View(comments);
        }
        public IActionResult DeleteComment(int id)
        {
            BlogComment comment = _context.BlogComments.FirstOrDefault(x => x.Id == id);
            if (comment is null) return NotFound();
            _context.BlogComments.Remove(comment);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(BlogPost post)
        {
            if (!ModelState.IsValid) return View(post);
            if (post.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Can't be null");
                return View(post);
            }
            if (post.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "Image size must be 2mb or less");
                return View(post);
            }
            if (post.ImageFile.ContentType != "image/png" && post.ImageFile.ContentType != "image/jpeg")
            {



                ModelState.AddModelError("ImageFile", "File must be jpeg, jpg or png");
                return View();
            }
            post.ImageUrl = post.ImageFile.SaveFile("uploads/posts", _env.WebRootPath);
            post.CreatedTime = DateTime.Now;
            _context.BlogPosts.Add(post);
            _context.SaveChanges();
            return RedirectToAction("index");

        }
        public IActionResult Update(int id)
        {
            BlogPost post = _context.BlogPosts.FirstOrDefault(x=>x.Id==id);
            if (post == null) return NotFound();
            return View(post);
        }
        [HttpPost]
        public IActionResult Update(BlogPost post)
        {
            BlogPost exstpost=_context.BlogPosts.FirstOrDefault(x=>x.Id==post.Id);
            if(exstpost == null) return NotFound();
            if (!ModelState.IsValid) return View(post);
            if (post.ImageFile != null)
            {
                if (post.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "Image size must be 2mb or less");
                    return View(post);
                }
                if (post.ImageFile.ContentType != "image/png" && post.ImageFile.ContentType != "image/jpeg")
                {



                    ModelState.AddModelError("ImageFile", "File must be jpeg, jpg or png");
                    return View();
                }
                string path = Path.Combine(_env.WebRootPath, "uploads/posts", exstpost.ImageUrl);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                exstpost.ImageUrl = post.ImageFile.SaveFile("uploads/posts", _env.WebRootPath);

            }
            exstpost.CreatedTime = exstpost.CreatedTime;
            exstpost.Desc1 = post.Desc1;
            exstpost.Desc2 = post.Desc2;
            exstpost.TittleDesc=post.TittleDesc;
            exstpost.AuthorName = post.AuthorName;
            exstpost.FbUrl = post.FbUrl;
            exstpost.IGUrl = post.IGUrl;
            exstpost.TTUrl = post.TTUrl;
            exstpost.Title= post.Title;
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult  Delete(int id)
        {
            BlogPost post = _context.BlogPosts.FirstOrDefault(x => x.Id == id);
            if (post == null) return NotFound();
            _context.BlogPosts.Remove(post);
            _context.SaveChanges();
            return RedirectToAction("index");

        }
    }
}
