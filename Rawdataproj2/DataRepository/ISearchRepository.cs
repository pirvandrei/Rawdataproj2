using DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel;
using DataRepository.Dto.SearchDto;

namespace DataRepository
{
    public interface ISearchRepository
    {
        Task<IList<BestmatchDto>> Bestmatch(string query, PagingInfo pagingInfo);
        Task<IList<MatchallDto>> MatchAll(string query);
        Task<IList<BestMatchRankedDto>> BestMatchRanked(string query);
        Task<IList<BestMatchWeightedDto>> BestMatchWeighted(string query);
    }
}
