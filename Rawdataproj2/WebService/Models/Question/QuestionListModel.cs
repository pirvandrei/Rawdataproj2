using System;
using System.Collections.Generic;
using DomainModel;
using WebService.Models.Note;
using WebService.Models.Tag;
using WebService.Models.User;

namespace WebService.Models.Question
{
    public class QuestionListModel
    {
        public string Url { get; set; } 
        public UserModel User { get; set; }

        public string Title { get; set; } 
        public int Score { get; set; }
        public DateTime CreationDate { get; set; }
        public string Body { get; set; }

        public IList<PostTagModel> PostTags { get; set; }
        //public IList<NoteModel> Notes { get; set; }
    }
}