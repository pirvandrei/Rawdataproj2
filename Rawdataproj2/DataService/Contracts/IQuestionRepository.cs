using DataService.Dto.QuestionDto;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface IQuestionRepository : IDataService<Question, int>
    {
           
        Task<Question> GetQuestion(int id);

		//Task<IEnumerable<QuestionAnswersDto>> GetQuestionAnswers(int id);
        //Task<IEnumerable<QuestionCommentsDto>> GetQuestionComments(int id);    
    }
}
