using System;
using System.Collections.Generic;

namespace DomainModel
{
    public class Question : Post
    { 
        public DateTime? ClosedDate { get; set; } 
        public int? AcceptedAnswerID { get; set; } 

        public User User { get; set; } 
        public IList<Answer> Answers { get; set; }
    }
}