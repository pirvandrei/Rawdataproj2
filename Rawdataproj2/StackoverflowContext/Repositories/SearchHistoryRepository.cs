using DataService;
using DomainModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackoverflowContext
{
    public class SearchHistoryRepository : ISearchHistoryRepository
    {
        public async Task<IEnumerable<Search>> GetAll(PagingInfo pagingInfo)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Searches.ToListAsync();
            }
        }

        public async Task<Search> Get(int userid)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Searches.FirstOrDefaultAsync(x => x.UserID == userid);
            }
        }

        public async Task<IEnumerable<Search>> GetSearchHistory(int userid, PagingInfo pagingInfo)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Searches
                    .Skip((pagingInfo.Page - 1) * pagingInfo.PageSize)
                    .Take(pagingInfo.PageSize)
                    .Where(x => x.UserID == userid).ToListAsync();
            }
        }

        public async Task<Search> Add(Search newSearch)
        { 
            using (var db = new StackoverflowDbContext())
            {
                await db.Searches.AddAsync(newSearch);
                db.SaveChanges();
                return newSearch;
            }
        }
          
        public async Task<bool> Delete(int userId)
        {  
            using (var db = new StackoverflowDbContext())
            {
                var search = await Get(userId);
                if (search == null) return false;
                db.Searches.Remove(search);
                await db.SaveChangesAsync();
                return true;
            }
        }

         

        public int Count()
        {
            using (var db = new StackoverflowDbContext())
            {
                return db.Searches.Count();
            }
        }

        public Task<bool> Update(Search b)
        {
            throw new NotImplementedException();
        }
    }
}
