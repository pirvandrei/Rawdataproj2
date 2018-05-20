using DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataRepository.Dto.SearchDto;
using System;

namespace DataRepository
{
    public interface ISearchRepository
    {
        Task<IList<SearchResultDto>> Bestmatch(string query, PagingInfo pagingInfo, string method, string startDate, string endDate);
        Task<Tuple<IList<SearchResultDto>, int>> MatchAll(string query, PagingInfo pagingInfo, string method, string startDate, string endDate);
        Task<IList<BestMatchRankedDto>> BestMatchRanked(string query);
        Task<IList<BestMatchWeightedDto>> BestMatchWeighted(string query);
    }
}
