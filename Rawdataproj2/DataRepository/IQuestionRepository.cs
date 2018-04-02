using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public interface IQuestionRepository : IDataRepository<Question, int>
    {

        Task<IEnumerable<Question>> GetAll(PagingInfo pagingInfo);
        Task<Question> Get(int id); 

        //Question GetQuestion(int id);
        //List<Question> GetQuestions();
    }
}
