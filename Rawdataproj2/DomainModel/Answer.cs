using System;  

namespace DomainModel
{
    public class Answer : Post
    { 
        public int QuestionID { get; set; } 
        public Question Question { get; set; }
    }
}
