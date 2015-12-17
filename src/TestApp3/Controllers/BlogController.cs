using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp3.Models;
using TestApp3.Models.Admin;
using TestApp3.Models.Blog;
using TestApp3.Models.Repository.Interfaces;

namespace TestApp3.Controllers
{
    public class BlogController : Controller
    {
        private readonly IRepository<Post> _postRepository;
        private readonly UserManager<User> _userManager;

        public BlogController(IRepository<Post> postRepository, UserManager<User> userManager)
        {
            _postRepository = postRepository;
            _userManager = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var posts = await _postRepository.GetResults();
            foreach (var post in posts)
            {
                await post.SetUser(_userManager);
            }
            var model = new BlogViewModel()
            {
                RecentPosts = posts.OrderByDescending(o => o.DatePublished).ToList()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ViewPost(string id)
        {
            var posts = await _postRepository.GetResults(p => p.Id == id);
            var post = posts.FirstOrDefault();

            if (post != null)
            {
                await post.SetUser(_userManager);
                return View(post);
            }
            return RedirectToAction(nameof(BlogController.Index));

        }

        public async Task<IActionResult> Tagged(string tag)
        {
            var posts = await _postRepository.GetResults(p => p.Tags.Contains(tag));
            foreach (var post in posts)
            {
                await post.SetUser(_userManager);
            }
            var model = new BlogViewModel()
            {
                RecentPosts = posts.OrderByDescending(o => o.DatePublished).ToList()
            };
            return View(nameof(BlogController.Index), model);
        }

        [HttpGet]
        public async Task<IActionResult> Comments(string id)
        {
            //TODO: create the view, logic
            return View();
        }
    }
}
