using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models.Search
{
    public class MatchallModel
    {
        public int Id { get; set; }
        public decimal Rank { get; set; }
        public string Body { get; set; }
    }
}
