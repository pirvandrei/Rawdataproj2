using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models.Post
{
    public class PostListModel
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public int Score { get; internal set; }
        public int Type { get; internal set; }
    }
}
