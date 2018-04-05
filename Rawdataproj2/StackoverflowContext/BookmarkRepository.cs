using DataRepository;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StackoverflowContext
{
    public class BookmarkRepository : IBookmarkRepository
    {
        public async Task<Bookmark> Get(int userId)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Bookmarks.FirstOrDefaultAsync(x => x.UserID == userId);
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

        public Task<bool> Update(int id, Bookmark b)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }
    }
}
