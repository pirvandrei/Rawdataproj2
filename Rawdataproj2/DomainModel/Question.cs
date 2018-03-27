using System;

namespace DomainModel
{
    public class Question
    {
        
        public string Title { get; set; } 
        public DateTime? Closeddate { get; set; }

        public int AcceptedAnswerID { get; set; } 
        public int ID { get; set; }
        public Post Post { get; set; }
    }
}