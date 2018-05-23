using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Dto.QuestionDto
{
    public class QuestionCommentsDto
    {
        public int ID { get; set; }
        public int Score { get; set; }
        public string Text { get; set; }
        public DateTime Creationdate { get; set; }
    }
}
