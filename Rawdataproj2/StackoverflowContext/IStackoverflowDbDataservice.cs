using System.Collections.Generic;
using DomainModel;

namespace StackoverflowContext
{
    public interface IStackoverflowDbDataservice
    {
        Question GetQuestion(int id);
        List<Question> GetQuestions();
    }
}