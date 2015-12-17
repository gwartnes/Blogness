using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp3.Models.Admin;

namespace TestApp3.Models.Blog
{
    public class BlogViewModel : PostViewModel
    {
        public List<Post> RecentPosts { get; set; }
    }
}
