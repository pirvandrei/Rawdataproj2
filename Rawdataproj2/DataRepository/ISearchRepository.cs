using DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataRepository.Dto.SearchDto;

namespace DataRepository
{
    public interface ISearchRepository
    {
        Task<IList<SearchResultDto>> Bestmatch(string query, PagingInfo pagingInfo);
        Task<IList<SearchResultDto>> MatchAll(string query);
        Task<IList<BestMatchRankedDto>> BestMatchRanked(string query);
        Task<IList<BestMatchWeightedDto>> BestMatchWeighted(string query);
    }
}
