﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using TestApp3.Models;
using Microsoft.AspNet.Authorization;
using TestApp3.Models.Admin;
using TestApp3.Models.Repository.Interfaces;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TestApp3.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<Post> _postRepository;

        public AdminController (UserManager<User> userManager, SignInManager<User> signInManager, IRepository<Post> postRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _postRepository = postRepository;
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
                var post = new Post() { Title = model.Title, Content = model.Body, Tags = model.Tags, UserName = User.Identity.Name };
                await _postRepository.InsertAsync(post);
            }
            else
            {
                return View(model);
            }

            return RedirectToAction("Index", "Blog");
        }

        [HttpGet]
        public async Task<IActionResult> EditPost(string id)
        {
            var model = new EditPostViewModel();

            if (string.IsNullOrEmpty(id))
            {
                var posts = await _postRepository.GetResults();
                model.RecentPosts = posts.OrderByDescending(o => o.DatePublished).ToList();
                return View("EditPostList", model);
            }
            else
            {
                var postToEdit = await _postRepository.GetResults(p => p.Id == id, 1);
                var post = postToEdit.First();

                model.Body = post.Content;
                model.Title = post.Title;
                //model.Tags = new string[post.Tags.Length];
                model.Tags = post.Tags;


                return View(model);
            }
        }
    }
}
