using DataService;
using DataService.Dto.NoteDto;
using DataService.Dto.PostDto;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackoverflowContext
{
    public class NoteRepository : INoteRepository
    {
        public User _user = new User { ID = 1, };
        public async Task<Note> Get(int postId)
        { 
            using (var db = new StackoverflowDbContext())
            {
                return await db.Notes
                    .Include(x => x.Post)
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.UserID == _user.ID && x.PostID == postId);
            }
        }

        public async Task<IEnumerable<Note>> GetAll(PagingInfo pagingInfo)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Notes
                    .Include(x => x.Post)
                    .Include(x => x.User)
                    .ToListAsync();
            }
        } 
  
        public async Task<bool> Delete(int id)
        {
            using (var db = new StackoverflowDbContext())
            {
                var note = await Get(id);
                if (note == null) return false;   
                db.Notes.Remove(note);
                await db.SaveChangesAsync();
                return true;
            }
        }
         
        public async Task<bool> Update(Note updateNote)
        {
            using (var db = new StackoverflowDbContext())
            {
                db.Notes.Update(updateNote);
                await db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<Note> Add(Note note)
        {
            using (var db = new StackoverflowDbContext())
            { 
                await db.Notes.AddAsync(note);
                db.SaveChanges();
                return note;
            }          
        }

        public int Count()
        {
            using (var db = new StackoverflowDbContext())
            {
                return  db.Notes.Count();
            }
        } 

		public async Task<IEnumerable<NoteDto>> GetNotes(PagingInfo pagingInfo)
        {
            using (var db = new StackoverflowDbContext())
            {             
    			return await 
    				(from n in db.Notes
                          join a in db.Answers on n.PostID equals a.ID
                          join r in db.Posts on a.ParentID equals r.ID
    		              select new NoteDto
                          {
                              ParentID = a.ParentID,
                              Title = r.Title,
                              PostID = a.ID,
                              UserID = n.UserID,
                              Posttype = a.PostType == 1 ? 
					                      "Question" : "Answer",
    			              Text = n.Text
				     }).Union(from n in db.Notes
                         join q in db.Questions on n.PostID equals q.ID
        			    	select new NoteDto
                             {                
                                 Title = q.Title,
                                 PostID = q.ID,
                                 UserID = n.UserID,
                                 Posttype = q.PostType == 1 ? 
					                         "Question" : "Answer",
        			             Text = n.Text
                             })
					.Skip((pagingInfo.Page - 1) * pagingInfo.PageSize)
                    .Take(pagingInfo.PageSize)
                    .ToListAsync();             
            }
        }
    }
}
