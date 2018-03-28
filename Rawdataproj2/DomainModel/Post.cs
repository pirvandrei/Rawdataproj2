using System;
using System.Collections.Generic;

namespace DomainModel
{
    public abstract class Post 
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int Score { get; set; }
        public DateTime CreationDate { get; set; }
        public string Body { get; set; }
        public int PostType { get; set; }
           
        public IList<Link> Links { get; set; }  
        public IList<PostTag> PostTags { get; set; }  
        public IList<Comment> Comments { get; set; } 


        //public Question Question { get; set; }
        //public IList<Answer> Answers { get; set; }
        //public IList<Question> Questions { get; set; }
    }
}
