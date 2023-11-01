using Medicoz.Models;
using Medicoz.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Medicoz.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult UserLogin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UserLogin(UserLoginVM userLoginVM)
        {
            if(!ModelState.IsValid) return View(userLoginVM);
            
            AppUser appUser =await _userManager.FindByNameAsync(userLoginVM.UserName);
            if(appUser == null)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(userLoginVM);
            }
            var result = await _signInManager.PasswordSignInAsync(appUser, userLoginVM.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View(userLoginVM);
            }
            return RedirectToAction("index", "home");
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegisterVM userRegisterVM)
        {
            if (!ModelState.IsValid) return View(userRegisterVM);
            AppUser appUser = null;
            appUser =await _userManager.FindByNameAsync(userRegisterVM.UserName);
            if (appUser != null)
            {
                ModelState.AddModelError("UserName", "Username already exists");
                return View(userRegisterVM);
            }
            appUser =await _userManager.FindByEmailAsync(userRegisterVM.Email);
            if (appUser != null)

            {
                ModelState.AddModelError("Email", "Email already exists");
                return View(userRegisterVM);
            }
            appUser = new AppUser
            {
                Email = userRegisterVM.Email,
                UserName = userRegisterVM.UserName,
                FullName = userRegisterVM.FullName,
                PhoneNumber = userRegisterVM.PhoneNumber
            };
            await _userManager.CreateAsync(appUser, userRegisterVM.Password);
            await _userManager.AddToRoleAsync(appUser,"Member");
            return RedirectToAction("userlogin", "account");

        }
        public async Task<IActionResult> UserLogout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
    }
}
