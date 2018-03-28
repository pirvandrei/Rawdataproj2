using System;
using System.Collections.Generic;

namespace DomainModel
{
    public class Question : Post
    { 
        public string Title { get; set; } 
        public DateTime? ClosedDate { get; set; } 
        public int? AcceptedAnswerID { get; set; } 

        public IList<Answer> Answers { get; set; }
    }
}