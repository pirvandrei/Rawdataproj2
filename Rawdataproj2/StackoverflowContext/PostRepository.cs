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
        public async Task<PostDto> Get(int id)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Questions
                    .Include(x => x.Answers)
                    .Select(x =>  new PostDto {
                    ID = x.ID,
                    Name = x.Title,
                    UserID = x.UserID,
                    Score = x.Score,
                    Body = x.Body,
                    Creationdate = x.CreationDate,
                    PostType = x.PostType 
                }).Where(x => x.ID == id).FirstOrDefaultAsync();
            }
        }

        public async Task<IEnumerable<PostDto>> GetAll(PagingInfo pagingInfo)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Questions
                    .Include(x => x.Answers)
                      .Select(x => new PostDto
                      {
                          ID = x.ID,
                          Name = x.Title,
                          UserID = x.UserID,
                          Score = x.Score,
                          Body = x.Body,
                          Creationdate = x.CreationDate,
                          PostType = x.PostType
                      })
                      .ToListAsync();
                  
            }
        }


        
         
        public void Add(Post b)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

       

        public Task<bool> Update(int id, Post b)
        {
            throw new NotImplementedException();
        } 
       

        public void Add(PostDto b)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(int id, PostDto b)
        {
            throw new NotImplementedException();
        }

       
    }
}
