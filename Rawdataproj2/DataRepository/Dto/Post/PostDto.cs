using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Dto.Post
{
    public class PostDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public DateTime Creationdate { get; set; }
        public string Body { get; set; }
    }
}
