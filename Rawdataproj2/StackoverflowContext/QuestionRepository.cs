using DataRepository;
using DataRepository.Dto.QuestionDto;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackoverflowContext
{
    public class QuestionRepository : IQuestionRepository
    {
        public async Task<Question> Get(int id)
        {
            using (var db = new StackoverflowDbContext())
            {
                 
                return await db.Questions.FirstOrDefaultAsync(x => x.ID == id);
            }
        }

        public async Task<IEnumerable<Question>> GetAll(PagingInfo pagingInfo)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Questions.ToListAsync();
            }
        }

        public async Task<Question> GetQuestion(int id)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Questions.
                    Include(x => x.Comments)
                    .Include(x=> x.Answers)
                    .ThenInclude(x => x.Comments) 
                 .FirstOrDefaultAsync(x => x.ID == id);
            }
        }

        public async Task<IEnumerable<QuestionCommentsDto>> GetQuestionComments(int id)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Comments
                    .Where(q => q.PostID == id)
                    .Select(q => new QuestionCommentsDto
                    {
                        ID = q.ID,
                        Score = q.Score,
                        Text = q.Text,
                        Creationdate = q.CreationDate,
                    })
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<QuestionAnswersDto>> GetQuestionAnswers(int id)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Answers
                    .Where(q => q.ParentID == id)
                    .Select(q => new QuestionAnswersDto
                    {
                        ID = q.ID,
                        Score = q.Score,
                        Body = q.Body,
                        Creationdate = q.CreationDate,  

                    }) 
                    .ToListAsync();
            }
        }
         

        public int Count()
        {
            using (var db = new StackoverflowDbContext())
            {
                return db.Questions.Count();
            }

        }

        public Task<Question> Add(Question b)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(int id, Question b)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
