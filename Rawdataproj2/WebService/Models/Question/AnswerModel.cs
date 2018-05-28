using System.Collections.Generic;

namespace WebService.Models.Question
{
    public class AnswerModel
    {
        public int ID { get; set; }
        //public int? ParentID { get; set; }
        public string UserName { get; set; }
        public string Body { get; set; }
        public int Score { get; set; } 
        public List<CommentModel> Comments { get; set; }
        public List<QuestionNoteModel> Notes { get; set; }
    }
}