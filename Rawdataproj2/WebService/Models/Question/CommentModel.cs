using DomainModel;
using System;
using WebService.Models.User;

namespace WebService.Models.Question
{
    public class CommentModel
    {
        //public int ID { get; set; }
        public DateTime CreationDate { get; set; }
        public int Score { get; set; }
        public string Text { get; set; }
         
       
        public string UserName { get; set; }
        //public UserModel User { get; set; }


        //public int PostID { get; set; }
        //public Post Post { get; set; }

    }
}