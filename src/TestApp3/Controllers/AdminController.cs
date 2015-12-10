using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using TestApp3.Models;
using Microsoft.AspNet.Authorization;
using TestApp3.Models.Admin;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TestApp3.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly UserManager<User> _userManager;

        public AdminController (UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {  
            return View();
        }

        [HttpGet]
        public IActionResult WritePost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WritePost(WritePostViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }

    }
}
