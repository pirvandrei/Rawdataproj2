using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository.Dto.Post
{
    public class AnswerDto
    {
        public int ID { get; set; }
        public int? QuestionID { get; set; }
        public string Title { get; set; }
        public int Score { get; set; }
    }
}
