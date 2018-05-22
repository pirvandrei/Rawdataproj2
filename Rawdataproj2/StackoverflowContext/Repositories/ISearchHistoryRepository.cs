using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public interface ISearchHistoryRepository : IDataRepository<Search, int>
    {
        Task<IEnumerable<Search>> GetSearchHistory(int userid);
        //bool DeletePeriodSearchHistory(DateTime from, DateTime to);
    }
}
