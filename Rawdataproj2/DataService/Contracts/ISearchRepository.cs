using DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataService.Dto.SearchDto;
using System;

namespace DataService
{
    public interface ISearchRepository
    {
        Task<Tuple<IList<SearchResultDto>, int>> BestMatchRanked(string query, PagingInfo pagingInfo, string startDate, string endDate);
        Task<Tuple<IList<SearchResultDto>, int>> MatchAll(string query, PagingInfo pagingInfo, string startDate, string endDate);
        Task<Tuple<IList<SearchResultDto>, int>> BestMatchWeighted(string query, PagingInfo pagingInfo, string startDate, string endDate);
    }
}
