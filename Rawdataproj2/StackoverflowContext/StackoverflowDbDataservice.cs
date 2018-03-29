using DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackoverflowContext
{
    public class StackoverflowDbDataservice : IStackoverflowDbDataservice
    {  
        public List<Question> GetQuestions()
        {
            using (var db = new StackoverflowDbContext())
            {
                var result = db.Questions 
                    //.Include(a => a.AcceptedAnswer) 
                    ;

                // .Include(x => x.Answers); 
                //.Include(x => x.PostTags)
                //.Include(x => x.Links);


                return result.ToList();  
            }
        }

        public Question GetQuestion(int id)
        {
            using (var db = new StackoverflowDbContext())
            {
                var question = db.Questions
                             //.Include(a => a.Answers) 
                             .Include(x => x.Answers)
                             .FirstOrDefault(x => x.ID == id);

                return question;
            }
        } 
    }
}
