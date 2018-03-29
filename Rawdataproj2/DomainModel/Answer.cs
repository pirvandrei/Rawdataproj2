using System;  

namespace DomainModel
{
    public class Answer : Post
    {
        public int? ParentID { get; set; }  
        public Question Parent { get; set; }

        //public int QuestionID { get; set; }  
        //public Question Question { get; set; }
       
    }
}
