using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models.Post
{
    public class PostModel : PostListModel
    {
        public int UserID { get; set; }
        public int Score { get; set; }
        public DateTime CreationDate { get; set; }
        public string Body { get; set; }
        public int PostType { get; set; }
    }
}
