using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blogness.Models;

namespace Blogness.Controllers
{
    public static class ControllerExtensions
    {
        public static async Task SetDisplayName(this Controller baseController, ClaimsPrincipal user, UserManager<User> userManager)
        {
            if (baseController.TempData["UserDisplayName"] == null || string.IsNullOrEmpty(baseController.TempData["UserDisplayName"].ToString()))
            {
                if (user.Identity.IsAuthenticated)
                {
                    var loggedInUser = await userManager.FindByNameAsync(user.Identity.Name);
                    baseController.TempData["UserDisplayName"] = loggedInUser.GetFirstName();
                }
            }
        }

        public static string ControllerName(this string controllerName)
        {
            return controllerName.Substring(0, controllerName.IndexOf("Controller"));
        }

        public static bool IsImage(this string fileName)
        {
            fileName = fileName.ToLower();
            return fileName.EndsWith(".png") || 
                fileName.EndsWith(".jpg") || 
                fileName.EndsWith(".jpeg") || 
                fileName.EndsWith(".gif");
        }
    }
}
