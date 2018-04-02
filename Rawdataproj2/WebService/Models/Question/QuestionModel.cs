using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models.Question
{
    public class QuestionModel : QuestionListModel
    {
        public DateTime? ClosedDate { get; set; }
        public int? AcceptedAnswerID { get; set; }
    }
}
