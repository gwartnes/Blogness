using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogness.Models;
using Blogness.Models.Repository.Interfaces;

namespace Blogness.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;

        public HomeController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            //await this.SetDisplayName(User, _userManager);
            return View();
        }

        public IActionResult GetLandingPage(string path)
        {
            return PhysicalFile(path, "text/html");
        }
    }
}
