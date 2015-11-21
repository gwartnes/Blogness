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
        //private BlogContext _context; //depends on MongoDb. define CRUD interfaces (repository) to get away from this, inject the interface

        private IRepository<Post> _postRepository;
        public HomeController(IRepository<Post> repository)
        {
            _postRepository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var results = await _postRepository.GetResults(null, 10, 0);
            var model = new IndexModel()
            {
                RecentPosts = results.OrderByDescending(o => o.DatePublishedUtc).ToList()
            };
            
            return View(model);
        }
    }
}
