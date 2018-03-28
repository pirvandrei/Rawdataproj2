using System;
using System.Collections.Generic;
using System.Text;
using DomainModel;

namespace DataRepository.Dto.Post
{
    public class QuestionDto 
    { 
        public int QuestionID { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public string Body { get; set; } 
        public DateTime Creationdate { get; set; }

        public string Title { get; set; } 
        public DateTime? ClosedDate { get; set; } 
        public Answer AcceptedAnswerID { get; set; }

        public IList<Answer> Answers { get; set; }
        public Answer AcceptedAnswer { get; set; }
    }
}
