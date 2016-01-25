using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TestApp3.Models.Account;
using Microsoft.AspNet.Identity;
using TestApp3.Models;
using Microsoft.AspNet.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TestApp3.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // GET: /<controller>/
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false); 
            
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                user.LastLoginDate = DateTime.Now;
                await _userManager.UpdateAsync(user);

                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid Username/password");
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterFirstTime()
        {
            return View("Register");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterFirstTime(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var adminUsers = await _userManager.GetUsersInRoleAsync("admin");
                if (adminUsers.Count == 0)
                {
                    var user = new User() { UserName = model.UserName, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
                    user.AddRole("admin");
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);

                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    AddErrors(result);
                }
                else
                {
                    ModelState.AddModelError("AdminAlreadyExists", "An admin user already exists. Try contacting the admin to set up an account.");
                }
                
            }
            return View("Register", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
