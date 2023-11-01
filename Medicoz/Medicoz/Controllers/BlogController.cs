using Medicoz.DAL;
using Medicoz.Models;
using Medicoz.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;


namespace Medicoz.Controllers
{
    
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BlogController(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            
            return View();
        }
        public async Task<IActionResult> Detail(int id)
        {
            AppUser member = null;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                member = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }
            

            BlogPost post=_context.BlogPosts.Include(x=>x.Comments).FirstOrDefault(x=>x.Id == id);
            if(post == null) return NotFound();
            post.TotalView ++;
            BlogComment comment = null;
            
            BlogVM vm = new BlogVM
            {
                Post = post,
                Comments = comment,
                UserName=member?.UserName,
                Email=member?.Email,
                
            };
            _context.SaveChanges();
            return View(vm);
        }
        [HttpPost]
        public  IActionResult Detail(int id,BlogVM blogVM)
        {

            BlogPost post = _context.BlogPosts.Include(x => x.Comments).FirstOrDefault(x => x.Id == id);
            if(post == null) return NotFound();
            blogVM.Comments.BlogPostId = post.Id;
            
            blogVM.Post=post;
            blogVM.Post.Id = post.Id;
            BlogVM postvm = new BlogVM
            {
                Comments = blogVM.Comments,
                Post = blogVM.Post,
                Email=blogVM.Email,
                UserName=blogVM.UserName,
            };
            //postvm.Comments.UserName = blogVM.UserName;
            postvm.Comments.Email = blogVM.Comments.Email;
            postvm.Comments.UserName = blogVM.Comments.UserName;
            
            
            BlogComment comment=blogVM.Comments;
            comment.UserName = blogVM.UserName;
            comment.Email = blogVM.Email;
            
            if (!ModelState.IsValid) return View(blogVM);
            comment.CommentTime=DateTime.Now;
            _context.BlogComments.Add(comment);
            _context.SaveChanges();


            return RedirectToAction("detail");
        }
    }
}
