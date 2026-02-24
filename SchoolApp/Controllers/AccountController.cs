using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Models;

namespace SchoolApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        // GET - Register form dikhao
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        // POST - Register form submit
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User"); // User role dega
                await _signInManager.SignInAsync(user, isPersistent: false);
                TempData["Success"] = "Account ban gaya! Welcome!";
                //("Action-Name","Controller-name")
                return RedirectToAction("Index", "Students");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model);
        }
        // GET - Login form dikhao
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        // POST - Login form submit
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
                
            var result = await _signInManager.PasswordSignInAsync(
               model.Email,
               model.Password,
               model.RememberMe,
               lockoutOnFailure: false
               );

            if (result.Succeeded)
            {
                TempData["Success"] = "Login successful! Welcome back!";
                return RedirectToAction("Index", "Students");
            }

            ModelState.AddModelError("", "Invalid login attempt! Email ya password galat hai.");
            return View(model);
        }
        // POST - Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["Success"] = "Logout successful! See you again!";
            return RedirectToAction("Login", "Account");
        }
        // GET - Access Denied
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}