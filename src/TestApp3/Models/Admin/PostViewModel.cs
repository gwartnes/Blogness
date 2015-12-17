using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TestApp3.Models.Blog;

namespace TestApp3.Models.Admin
{
    public class PostViewModel
    {
        private string[] _tags;

        public string Id { get; set; }
        [Display(Name = "Post Title")]
        public string Title { get; set; }
        [Display(Name = "Post Body")]
        public string Body { get; set; }
        public string[] Tags {
            get
            {
                return _tags;
            }
            set
            {
                _tags = new string[value.Length];
                for (int i = 0; i < value.Length; i++)
                {
                    _tags[i] = value[i].Replace(' ', '-');
                } 
            }
        }

        public Comment[] Comments { get; set; }
        public DateTime DateCreated { get; set; }
        public string[] Images { get; set; }
    }
}
