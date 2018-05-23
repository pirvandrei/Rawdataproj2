using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataService.Repositories
{
    interface IStatisticsRepository
    {
        Task<IList<object>> RankedWordList(int id);
        Task<IList<object>> WeightedWordList(int id);
        Task<IList<object>> GetAssociations(int id);
        Task<IList<object>> TermNetwork(int id);
    }
}
