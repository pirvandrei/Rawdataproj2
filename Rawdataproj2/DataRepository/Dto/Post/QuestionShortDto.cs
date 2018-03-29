using System;
using System.Collections.Generic;
using System.Text;
using DomainModel;

namespace DataRepository.Dto.Post
{
    public class QuestionShortDto
    {
        public int QuestionID { get; set; } 
        public string Title { get; set; }
        public int Score { get; set; }
        //public IList<Answer> Answers { get; set; }
        public int? AcceptedAnswerID { get; set; }
    }
}
