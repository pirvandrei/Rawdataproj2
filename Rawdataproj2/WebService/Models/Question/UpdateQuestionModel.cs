using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models.Question
{
    public class UpdateQuestionModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        //public DateTime ClosedDate { get; set; }
        public int Score { get; set; }
        public int? AcceptedAnswerID { get; set; }

    }
}
