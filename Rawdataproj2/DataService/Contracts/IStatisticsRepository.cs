using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataService.Dto.StatisticsDto;

namespace DataService
{
    public interface IStatisticsRepository
    {
        Task<IList<RankedWordListDto>> RankedWordList(string word);
        Task<IList<WeightedWordListDto>> WeightedWordList(string term);
        Task<IList<AssociationsListDto>> GetAssociations(string word);
        Task<TermNetworkDto> TermNetwork(string word, double grade);
    }
}
