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
    public class SearchHistoryRepository : ISearchHistoryRepository
    {
        public async Task<IList<Search>> GetAll(PagingInfo pagingInfo)
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
                return await db.Searches.FirstOrDefaultAsync(x => x.ID == userid);
            }
        }

        public async Task<IEnumerable<Search>> GetSearchHistory(int userid)
        {
            using (var db = new StackoverflowDbContext())
            {
                return await db.Searches.Where(x => x.ID == userid).ToListAsync();
            }
        }

        public async void Add(Search newSearch)
        {
            using (var db = new StackoverflowDbContext())
            {
                await db.Searches.AddAsync(newSearch);
            }
        }

        public async Task<bool> Delete(int userid)
        {
            using (var db = new StackoverflowDbContext())
            {
                var history = await Get(userid);
                if (history == null) return false;
                db.Searches.Remove(history);
                await db.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> Update(int userid, Search updateSearch)
        {
            using (var db = new StackoverflowDbContext())
            {
                var search = await Get(userid);
                if (search == null) return false;
                search.Text = updateSearch.Text;
                search.Date = updateSearch.Date;
                await db.SaveChangesAsync();
                return true;
            }
        }
    }
}
