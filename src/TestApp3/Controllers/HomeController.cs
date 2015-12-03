﻿using Microsoft.AspNet.Identity;
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
        private readonly IRepository<Post> _postRepository;
        private readonly UserManager<User> _userManager;

        public HomeController(IRepository<Post> postRepository, UserManager<User> userManager)
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
                post.User = await _userManager.FindByIdAsync(post.UserId);              
            }
            var model = new IndexModel()
            {
                RecentPosts = posts.OrderByDescending(o => o.DatePublished).ToList()
            };
            
            return View(model);
        }
    }
}
