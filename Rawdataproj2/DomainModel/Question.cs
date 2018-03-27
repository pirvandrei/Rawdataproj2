using System;

namespace DomainModel
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public DateTime Closeddate { get; set; }

        public Answer AcceptedAnswer { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}