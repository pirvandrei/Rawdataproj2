using System.Collections.Generic;
using WebService.Models.Bookmark;

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
		//public IList<BookmarkModel> Bookmarks { get; set; }
        public bool Bookmarked { get; set; }
    }
}