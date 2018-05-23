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
    public class BookmarkRepository : IBookmarkRepository
    {
        public User _user = new User { ID = 1, };

        public async Task<Bookmark> Get(int postId)
        { 
            using (var db = new StackoverflowDbContext())
            {
                return await db.Bookmarks
                    .FirstOrDefaultAsync(x => x.UserID == _user.ID && x.PostID == postId);
            }
        }

        public async Task<IEnumerable<Bookmark>> GetAll(PagingInfo pagingInfo)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Bookmarks.ToListAsync();
            }
        }

        //public async Task<Bookmark> Add(int id, Bookmark bookmark)
        //{
        //    using (var db = new StackoverflowDbContext())
        //    {
        //        bookmark.UserID = _user.ID;
        //        bookmark.PostID = id;
        //        await db.Bookmarks.AddAsync(bookmark);
        //        db.SaveChanges();
        //        return bookmark;
        //    }
        //}

        public async Task<bool> Delete(int postId)
        {
            using (var db = new StackoverflowDbContext())
            {
                var bookmark = await Get(postId);
                if (bookmark == null) return false;
                db.Bookmarks.Remove(bookmark);
                await db.SaveChangesAsync();
                return true;
            }
        }


        public async Task<Bookmark> Add(Bookmark bookmark)
        {
            using (var db = new StackoverflowDbContext())
            {
                await db.Bookmarks.AddAsync(bookmark);
                db.SaveChanges();
                return bookmark;
            }

        }

        public int Count()
        {
            using (var db = new StackoverflowDbContext())
            {
                return db.Bookmarks.Count();
            }
        }

        public Task<bool> Update(Bookmark b)
        {
            throw new NotImplementedException();
        }
    }
}
