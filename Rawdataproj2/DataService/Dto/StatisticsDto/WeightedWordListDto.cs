using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Dto.StatisticsDto
{
    public class WeightedWordListDto
    {
        public string Term { get; set; }
        public decimal Rank { get; set; }
    }
}
