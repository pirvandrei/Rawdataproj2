using DataRepository;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks;

namespace StackoverflowContext
{
    public class CommentRepository : ICommentRepository
    {
        public async void Add(Comment comment)
        { 
            using (var db = new StackoverflowDbContext())
            {
                await db.Comments.AddAsync(comment);
            }
        } 

        public async Task<Comment> Get(int id)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Comments.FirstOrDefaultAsync(x => x.ID == id);
            }
        }

        public async Task<IEnumerable<Comment>> GetAll(PagingInfo pagingInfo)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Comments.ToListAsync();
            }
        }

        public async Task<bool> Update(int id, Comment com)
        {
            using (var db = new StackoverflowDbContext())
            {
                var comment = await Get(id);
                if (comment == null) return false;
                db.Comments.Update(com);
                await db.SaveChangesAsync();
                return true;
            }
        }


        public async Task<bool> Delete(int id)
        {
            using (var db = new StackoverflowDbContext())
            { 
                var comment = await Get(id);
                if (comment == null) return false;
                db.Comments.Remove(comment);
                await db.SaveChangesAsync();
                return true;
            }
        }
    }
}




 