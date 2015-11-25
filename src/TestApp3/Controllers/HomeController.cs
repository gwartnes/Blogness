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
        private IRepository<Post> _postRepository;
        private IRepository<User> _userRepository;
        public HomeController(IRepository<Post> postRepository, IRepository<User> userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var posts = await _postRepository.GetResults(null, 10, 0);
            foreach (var post in posts)
            {
                var userList = await _userRepository.GetResults(u => u.Id == post.UserId, 1);
                post.User = userList.FirstOrDefault();
            }
            var model = new IndexModel()
            {
                RecentPosts = posts.OrderByDescending(o => o.DatePublished).ToList()
            };
            
            return View(model);
        }
    }
}
