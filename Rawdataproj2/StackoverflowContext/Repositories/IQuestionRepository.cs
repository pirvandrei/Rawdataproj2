using DataRepository.Dto.QuestionDto;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public interface IQuestionRepository : IDataRepository<Question, int>
    {
        Task<IEnumerable<QuestionAnswersDto>> GetQuestionAnswers(int id);
        Task<IEnumerable<QuestionCommentsDto>> GetQuestionComments(int id);

        Task<Question> GetQuestion(int id);
    }
}
