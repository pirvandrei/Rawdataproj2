using DataRepository;
using DataRepository.Dto.PostDto;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackoverflowContext
{
    public class PostRepository : IPostRepository
    {
          
        public async Task<IEnumerable<Question>> GetAll(PagingInfo pagingInfo)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Questions
                    .Include(x => x.Answers)
                    .Skip(pagingInfo.Page * pagingInfo.PageSize)
                    .Take(pagingInfo.PageSize)
                    .ToListAsync();

                //.Include(x => x.PostTags)
                //.Include(x => x.Links); 
            }
        }

        public async Task<Question> Get(int id)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Questions
                    .Include(x => x.Answers)
                    .Include(x => x.Comments)
                    .FirstOrDefaultAsync(x => x.ID == id);

                //.Include(x => x.PostTags)
                //.Include(x => x.Links);

            }
        }

        public async void Add(Question b)
        {
            using (var db = new StackoverflowDbContext())
            {
                await db.Questions.AddAsync(b);
            }
        }

        public async Task<bool> Update(int id, Question quest)
        {
            using (var db = new StackoverflowDbContext())
            {
                var question = await Get(id);
                if (question == null) return false;
                //question.Body = quest.Body;
                question.ClosedDate = quest.ClosedDate;
                await db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (var db = new StackoverflowDbContext())
            {
                var question = await Get(id);
                if (question == null) return false;
                db.Questions.Remove(question);
                await db.SaveChangesAsync();
                return true;
            }
        }

 

    }
}
