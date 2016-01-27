using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogness.Models.Admin;

namespace Blogness.Models.Blog
{
    public class BlogViewModel : PostViewModel
    {
        public List<Post> RecentPosts { get; set; }
    }
}
