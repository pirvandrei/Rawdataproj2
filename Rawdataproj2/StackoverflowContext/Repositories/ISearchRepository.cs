using DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataRepository.Dto.SearchDto;
using System;

namespace DataRepository
{
    public interface ISearchRepository
    {
        Task<Tuple<IList<SearchResultDto>, int>> BestMatchRanked(string query, PagingInfo pagingInfo, string startDate, string endDate);
        Task<Tuple<IList<SearchResultDto>, int>> MatchAll(string query, PagingInfo pagingInfo, string startDate, string endDate);
        Task<Tuple<IList<SearchResultDto>, int>> BestMatchWeighted(string query, PagingInfo pagingInfo, string startDate, string endDate);
    }
}
