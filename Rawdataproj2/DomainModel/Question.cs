using System;
using System.Collections.Generic;

namespace DomainModel
{
    public class Question : Post
    { 
        public string Title { get; set; } 
        public DateTime? Closeddate { get; set; } 
        public int AcceptedanswerID { get; set; }  

        //public IList<Answer> Answers { get; set; }
    }
}