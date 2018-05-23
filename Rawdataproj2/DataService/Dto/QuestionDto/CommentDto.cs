using System;
using System.Collections.Generic;
using System.Text;
using DomainModel;

namespace DataService.Dto.PostDto
{
    public class CommentDto
    {
        public int ID { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public string Text { get; set; }

        //public int UserID { get; set; }
        //public User User { get; set; }

        public int PostID { get; set; }
        public Post Post { get; set; }
    }
}
