using DataService;
using DataService.Dto.QuestionDto;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;  

namespace StackoverflowContext
{
	public class QuestionRepository : IQuestionRepository
	{
		public async Task<Question> Get(int id)
		{
			using (var db = new StackoverflowDbContext())
			{
				return await db.Questions.FirstOrDefaultAsync(x => x.ID == id);
			}
		}

		public async Task<IEnumerable<Question>> GetAll(PagingInfo pagingInfo)
		{
			using (var db = new StackoverflowDbContext())
			{
				return await db.Questions
					.Include(x => x.PostTags)
					.Include(x => x.User)
                    //.Include(x => x.Notes)
					.Skip((pagingInfo.Page - 1) * pagingInfo.PageSize)
					.Take(pagingInfo.PageSize)
					.ToListAsync();
			}
		}

		public async Task<Question> GetQuestion(int id)
		{
			using (var db = new StackoverflowDbContext())
			{
				return await db.Questions   
    				   .Include(x => x.Comments)
    							.ThenInclude(x => x.User) 
                       .Include(x => x.Answers)
    							.ThenInclude(x => x.Comments)
    							    .ThenInclude(x => x.User)
                        .Include(x => x.Answers)
                                .ThenInclude(x => x.Notes)
                        .Include(x => x.Answers)
                                .ThenInclude(x => x.User)
	                    .Include(x => x.Answers)
                            .ThenInclude(x => x.Bookmarks)
                       .Include(x => x.PostTags)
					   .Include(x => x.Bookmarks)
					           .ThenInclude(x => x.User)
                       .Include(x => x.Notes) 
				   .FirstOrDefaultAsync(x => x.ID == id);    
			}
		}




		public int Count()
		{
			using (var db = new StackoverflowDbContext())
			{
				return db.Questions.Count();
			}
		}

		public async Task<bool> Delete(int id)
		{
			using (var db = new StackoverflowDbContext())
			{
				var q = await Get(id);
				if (q == null) return false;
				db.Questions.Remove(q);
				await db.SaveChangesAsync();
				return true;
			}
		}

		public async Task<bool> Update(Question q)
		{
			using (var db = new StackoverflowDbContext())
			{
				db.Questions.Update(q);
				await db.SaveChangesAsync();
				return true;
			}
		}

		public async Task<Question> Add(Question q)
		{
			using (var db = new StackoverflowDbContext())
			{
				await db.Questions.AddAsync(q);
				db.SaveChanges();
				return q;
			}

		}

		//public async Task<IEnumerable<QuestionCommentsDto>> GetQuestionComments(int id)
        //{
        //  using (var db = new StackoverflowDbContext())
        //  {
        //      return await db.Comments
        //          .Where(q => q.PostID == id)
        //          .Select(q => new QuestionCommentsDto
        //          {
        //              ID = q.ID,
        //              Score = q.Score,
        //              Text = q.Text,
        //              Creationdate = q.CreationDate,
        //          })
        //          .ToListAsync();
        //  }
        //}

        //public async Task<IEnumerable<QuestionAnswersDto>> GetQuestionAnswers(int id)
        //{
        //  using (var db = new StackoverflowDbContext())
        //  {
        //      return await db.Answers
        //          .Where(q => q.ParentID == id)
        //          .Select(q => new QuestionAnswersDto
        //          {
        //              ID = q.ID,
        //              Score = q.Score,
        //              Body = q.Body,
        //              Creationdate = q.CreationDate,

        //          })
        //          .ToListAsync();
        //  }
        //}
	}
}
