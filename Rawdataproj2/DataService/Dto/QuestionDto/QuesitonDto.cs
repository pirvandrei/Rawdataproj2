using DataService.Dto.PostDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Dto.QuestionDto
{
    public class QuestionDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int UserID { get; set; }
        public int Score { get; set; }
        public string Body { get; set; }
        public DateTime Creationdate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public int? AcceptedAnswerID { get; set; }

        public IList<CommentDto> Comments { get; set; }
        public IList<AnswerDto> Answers { get; set; }
        
    }
}
