using System;

namespace DomainModel
{
    public class Search
    {
        
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

    }
}