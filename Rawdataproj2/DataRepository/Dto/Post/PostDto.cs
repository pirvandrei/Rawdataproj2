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
        public DateTime Creationdate { get; set; }
        public string Body { get; set; }
         
        public string Title { get; set; } 
        public DateTime? Closeddate { get; set; } 
        public int AcceptedanswerID { get; set; }  
         
    }
}
