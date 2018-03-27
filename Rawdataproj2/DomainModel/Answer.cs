using System;  

namespace DomainModel
{
    public class Answer
    { 
        public int ParentId { get; set; } 
        public int ID { get; set; }
        public Post Post { get; set; }
    }
}
