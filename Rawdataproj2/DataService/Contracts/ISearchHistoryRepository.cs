using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface ISearchHistoryRepository : IDataService<Search, int>
    {
        Task<IEnumerable<Search>> GetSearchHistory(int userid, PagingInfo pagingInfo);
        //bool DeletePeriodSearchHistory(DateTime from, DateTime to);
    }
}
