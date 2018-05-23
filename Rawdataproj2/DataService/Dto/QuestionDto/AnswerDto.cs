using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Dto.PostDto
{
    public class AnswerDto
    {
        public int ID { get; set; }
        public int? ParentID { get; set; }
        public string Body { get; set; }
        public int Score { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}
