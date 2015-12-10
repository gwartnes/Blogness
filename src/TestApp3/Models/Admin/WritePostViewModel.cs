using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp3.Models.Admin
{
    public class WritePostViewModel
    {
        [Display(Name = "Post Title")]
        public string Title { get; set; }
        [Display(Name = "Post Body")]
        public string Body { get; set; }
        public string[] Tags { get; set; }
    }
}
