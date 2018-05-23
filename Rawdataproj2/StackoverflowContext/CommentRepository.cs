using DataService;
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
        public User _user = new User { ID = 1, };
        public async Task<Comment> Get(int postId)
        { 
            using (var db = new StackoverflowDbContext())
            {
                return await db.Comments
                    .FirstOrDefaultAsync(x => x.PostID == postId && x.UserID == _user.ID);
            }
        }
         
        public async Task<IEnumerable<Comment>> GetAll(PagingInfo pagingInfo)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Comments.ToListAsync();
            }
        }
         
        public async Task<Comment> Add(int id, Comment comment)
        {
            using (var db = new StackoverflowDbContext())
            {
                comment.UserID = _user.ID;
                comment.PostID = id;
                await db.Comments.AddAsync(comment);
                db.SaveChanges();
                return comment;
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

        public async Task<bool> Update(Comment com)
        {
            using (var db = new StackoverflowDbContext())
            {
                db.Comments.Update(com);
                await db.SaveChangesAsync();
                return true;
            }
        }

        public Task<Comment> Add(Comment b)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }
    }
}




 