using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp3.Models.Admin
{
    public class UnapprovedCommentsModel
    {
        public UnapprovedCommentsModel()
        {
            UnapprovedComments = new Dictionary<Comment, Post>();
        }
        public Dictionary<Comment, Post> UnapprovedComments { get; set; }
    }
}
