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
    public class BookmarkRepository : IBookmarkRepository
    {
        public async Task<Bookmark> Get(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Bookmark> Get(int userId, int postId)
        { 
            using (var db = new StackoverflowDbContext())
            {
                return await db.Bookmarks.FirstOrDefaultAsync(x => x.UserID == userId && x.PostID == postId);
            }
        }

        public async Task<IEnumerable<Bookmark>> GetAll(PagingInfo pagingInfo)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Bookmarks.ToListAsync();
            }
        }

        public async void Add(Bookmark bookmark)
        {
            using (var db = new StackoverflowDbContext())
            {
                await db.Bookmarks.AddAsync(bookmark);
            }
        }

        public int Count()
        {
            using (var db = new StackoverflowDbContext())
            {
                return db.Bookmarks.Count();
            }
        }

        public async Task<bool> Delete(int userId)
        {
            using (var db = new StackoverflowDbContext())
            {
                var bookmark = await Get(userId);
                if (bookmark == null) return false;
                db.Bookmarks.Remove(bookmark);
                await db.SaveChangesAsync();
                return true;
            }
        }



        public async Task<bool> Update(int userId, Bookmark userBookmark)
        {
            using (var db = new StackoverflowDbContext())
            {
                var bookmark = await Get(userId);
                if (bookmark == null) return false;
                //TODO: update bookmark attributes
                db.Bookmarks.Update(userBookmark);
                await db.SaveChangesAsync();
                return true;
            }
        }

        
    }
}
