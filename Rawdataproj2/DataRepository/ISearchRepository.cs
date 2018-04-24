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
    }
}
