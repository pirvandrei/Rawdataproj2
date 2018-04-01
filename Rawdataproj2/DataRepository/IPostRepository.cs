using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public interface IPostRepository : IDataRepository<Question, int>
    {

        //Task<IEnumerable<Question>> GetAll();
        //Task<Question> Get(int id); 

        //Question GetQuestion(int id);
        //List<Question> GetQuestions();
    }
}
