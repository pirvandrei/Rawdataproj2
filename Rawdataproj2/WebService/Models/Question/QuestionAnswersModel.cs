using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models.Question
{
    public class QuestionAnswersModel 
    { 
        public int Score { get; set; }
        public string Body { get; set; }
        public DateTime Creationdate { get; set; } 
    }
}
