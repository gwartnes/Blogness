﻿using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestApp3.Models;
using TestApp3.Models.Admin;
using TestApp3.Models.Repository.Interfaces;

namespace TestApp3.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<Post> _postRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AdminController (
            UserManager<User> userManager, 
            SignInManager<User> signInManager, 
            IRepository<Post> postRepository, 
            IHostingEnvironment hostingEnvironment)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _postRepository = postRepository;
            _hostingEnvironment = hostingEnvironment;
        }

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

            return RedirectToAction(nameof(BlogController.Index), nameof(BlogController));
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
                model.Id = post.Id;
                model.Tags = post.Tags;

                return View(model);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> EditPost(EditPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var postToEdit = await _postRepository.GetResults(p => p.Id == model.Id, 1);
                var post = postToEdit.First();

                post.Title = model.Title;
                post.Content = model.Body;
                post.Tags = model.Tags;
                post.DateUpdated = DateTime.Now;

                await _postRepository.UpdateAsync(post);
            }
            else
            {
                return View(model);
            }

            return RedirectToAction(nameof(BlogController.Index), nameof(BlogController));
        }

        [HttpPost]
        public async Task Upload(IFormFile file)
        {        
            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

            if (fileName.EndsWith(".png") || fileName.EndsWith(".jpg") || fileName.EndsWith(".jpeg") || fileName.EndsWith(".gif"))
            {
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, string.Format("img\\"));
                var savePath = Path.Combine(filePath, fileName);
                await file.SaveAsAsync(savePath);
            }
        }
    }
}
