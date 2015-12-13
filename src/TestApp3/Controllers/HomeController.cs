using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp3.Models;
using TestApp3.Models.Home;
using TestApp3.Models.Repository.Interfaces;

namespace TestApp3.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetLandingPage(string path)
        {
            return new FilePathResult(path, "text/html");
        }
    }
}
