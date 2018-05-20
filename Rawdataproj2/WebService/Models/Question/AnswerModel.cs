using System.Collections.Generic;
using WebService.Models.Note;
using WebService.Models.User;

namespace WebService.Models.Question
{
    public class AnswerModel  
    {
        public int ID { get; set; }
         public int? ParentID { get; set; }
        public string Body { get; set; }
        public int Score { get; set; }
        public UserModel User { get; set; }
        public IList<CommentModel> Comments { get; set; }
        public IList<NoteModel> Notes { get; set; }

    }
}