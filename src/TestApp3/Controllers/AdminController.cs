using Microsoft.AspNet.Authorization;
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
using TestApp3.Models.Blog;
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
            var model = new PostViewModel { Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString() };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WritePost(PostViewModel model)
        {
            if (ModelState.IsValid)
            {
                var post = new Post() { Id = model.Id, Title = model.Title, Content = model.Body, Tags = model.Tags, UserName = User.Identity.Name };
                await _postRepository.InsertAsync(post);
            }
            else
            {
                return View(model);
            }

            return RedirectToAction(nameof(BlogController.Index), nameof(BlogController).ControllerName());
        }

        [HttpGet]
        public async Task<IActionResult> EditPost(string id)
        {
            var model = new BlogViewModel();

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

                if (post == null)
                {
                    return RedirectToAction(nameof(AdminController.Index));
                }

                model.Body = post.Content;
                model.Title = post.Title;
                model.Id = post.Id;
                model.Tags = post.Tags;

                var filePath = GetFilePath(id);

                model.Images = Directory.Exists(filePath) 
                    ? Directory.GetFiles(filePath)
                        .Select(path => path.Substring(path.IndexOf("\\img\\"), path.Length - path.IndexOf("\\img\\")))
                        .ToArray() 
                    : new string[0];
               
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(BlogViewModel model)
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

            return RedirectToAction(nameof(BlogController.Index), nameof(BlogController).ControllerName());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var id = Request.Form["id"];

            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var filePath = GetFilePath(id);

            if (fileName.IsImage())
            {
                Directory.CreateDirectory(filePath);

                var savePath = Path.Combine(filePath, fileName);
                await file.SaveAsAsync(savePath);
            }
            return Json(new { FileName = fileName, FilePath = filePath, Success = true });
        }

        [HttpGet]
        public async Task<IActionResult> Comments()
        {
            var postsWithComments = await _postRepository.GetResults(p => p.Comments != null);
            var model = new List<UnapprovedCommentModel>();

            foreach (var post in postsWithComments)
            {
                foreach (var comment in post.Comments)
                {
                    if (!comment.Approved)
                    {
                        model.Add(new UnapprovedCommentModel { Comment = comment, Post = post });
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Comments(IEnumerable<UnapprovedCommentModel> model)
        {

            foreach (var unapprovedComment in model)
            {
                if (unapprovedComment.Post != null && unapprovedComment.Post.Id != null)
                {
                    var postResults = await _postRepository.GetResults(p => p.Id == unapprovedComment.Post.Id, 1);
                    var postToUpdate = postResults.FirstOrDefault();

                    var comment = postToUpdate?.Comments?.FirstOrDefault(c => c?.Id == unapprovedComment?.Comment?.Id);
                    if (comment != null)
                    {
                        if (unapprovedComment.Approved)
                        {
                            comment.Approved = true;
                        }
                        else
                        {
                            postToUpdate.Comments.Remove(comment);
                        }
                    }
                    await _postRepository.UpdateAsync(postToUpdate);
                }
                else
                {
                    return View(model);
                }
            }       
            return RedirectToAction(nameof(AdminController.Index), nameof(AdminController).ControllerName());
        }

        private string GetFilePath(string id)
        {
            return Path.Combine(_hostingEnvironment.WebRootPath, string.Format("img\\{0}\\", id));
        }
    }
}
