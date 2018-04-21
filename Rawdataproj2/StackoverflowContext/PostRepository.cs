using DataRepository;
using DataRepository.Dto.PostDto;
using DomainModel;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackoverflowContext
{
    public class PostRepository : IPostRepository
    {
        public async Task<Post> Get(int id)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Posts
                    .FirstOrDefaultAsync(x => x.ID == id);
            }
        }

        public async Task<IEnumerable<Post>> GetAll(PagingInfo pagingInfo)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Questions
                    .Include(x => x.Answers)
                    .Skip(pagingInfo.Page * pagingInfo.PageSize)
                    .Take(pagingInfo.PageSize)
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

        public Task<Post> Add(Post b)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Post b)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
