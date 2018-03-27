using System;
using System.Collections.Generic;

namespace DomainModel
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public DateTime CreationDate { get; set; }
        public string Body { get; set; }

         

        public IList<Link> Links { get; set; }

        public IList<Answer> Answers{ get; set; }
        public IList<Question> Questions { get; set; }
        public IList<PostTag> PostTags { get; set; } 

       
    }
}
