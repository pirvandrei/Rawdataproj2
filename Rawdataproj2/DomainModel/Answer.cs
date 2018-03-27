using System;  

namespace DomainModel
{
    public class Answer : Post
    { 
        public int ParentID { get; set; } 
    }
}
