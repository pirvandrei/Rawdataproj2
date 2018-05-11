using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models.Tag;
using WebService.Models.User;

namespace WebService.Models.Question
{
    public class QuestionModel 
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int UserID { get; set; }
        public UserModel User { get; set; }

        public int Score { get; set; }
        public string Body { get; set; }
        public DateTime Creationdate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public int? AcceptedAnswerID { get; set; }
          
        public IList<CommentModel> Comments { get; set; }
        public IList<AnswerModel> Answers { get; set; }
        public IList<PostTagModel> PostTags { get; set; }
    }
}
