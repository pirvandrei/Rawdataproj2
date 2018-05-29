using DataService;
using DataService.Dto.BookmarkDto;
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
                return await db.Bookmarks
				               .Include(x=>x.Post)
					           .ToListAsync();
            }
        }        

		public async Task<IEnumerable<BookmarkDto>> GetBookmarks(PagingInfo pagingInfo)
        {
            using (var db = new StackoverflowDbContext())
            {             
                return await (from b  in db.Bookmarks 
                                  join  a in db.Answers on b.PostID equals a.ID
                                  join r in db.Posts on a.ParentID equals r.ID 
                                  select new BookmarkDto
                                     {
                                         ParentID = a.ParentID,
                                         Title = r.Title,
                                         PostID = a.ID,
                                         UserID = b.UserID,
                                         Posttype = a.PostType == 1 ? "Question" : "Answer"
                                     }
                             )
                            .Union(
                             from b in db.Bookmarks
                                join q in db.Questions on b.PostID equals q.ID 
                                    select new BookmarkDto
                                     { 
                                         Title = q.Title,
                                         PostID = q.ID,
                                         UserID = b.UserID,
                                         Posttype = q.PostType == 1 ? "Question" : "Answer"
                                     }
                            ).Skip((pagingInfo.Page - 1) * pagingInfo.PageSize)
                                    .Take(pagingInfo.PageSize)
                                    .ToListAsync(); 
            }
        } 

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
