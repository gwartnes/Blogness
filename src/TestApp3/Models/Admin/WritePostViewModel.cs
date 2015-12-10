using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp3.Models.Admin
{
    public class WritePostViewModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string[] Tags { get; set; }
    }
}
