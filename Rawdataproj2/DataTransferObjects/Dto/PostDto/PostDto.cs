using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository.Dto.PostDto
{
    public class PostDto
    {
        public int ID { get; set; } 
        public int UserID { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public DateTime Creationdate { get; set; }
        public int PostType { get; set; }
    }
}
