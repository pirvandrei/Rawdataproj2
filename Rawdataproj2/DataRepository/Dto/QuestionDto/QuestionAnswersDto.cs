using System;
using System.Collections.Generic;
using System.Text;

namespace DataRepository.Dto.QuestionDto
{
    public class QuestionAnswersDto
    {
        public int Score { get; set; }
        public string Body { get; set; }
        public DateTime Creationdate { get; set; }
        public int ID { get; set; }
    }
}
