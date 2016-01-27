using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blogness.Models.Admin
{
    public class UnapprovedCommentModel
    {
        public Comment Comment { get; set; }
        public Post Post { get; set; }
        [Display(Name = "")]
        public bool Approved { get; set; }
    }
}
