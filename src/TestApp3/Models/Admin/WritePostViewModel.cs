using MarkdownSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp3.Models.Admin
{
    public class WritePostViewModel
    {
        private string _body;

        [Display(Name = "Post Title")]
        public string Title { get; set; }
        [Display(Name = "Post Body")]
        public string Body {
            get
            {
                return _body;
            }
            set
            {
                var markdown = new Markdown();
                _body = markdown.Transform(value);
            }
        }
        public string[] Tags { get; set; }
    }
}
