using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp3.Models.Admin
{
    public class EditPostViewModel : WritePostViewModel
    {
        public List<Post> RecentPosts { get; set; }

        public string Id { get; set; }
    }
}
