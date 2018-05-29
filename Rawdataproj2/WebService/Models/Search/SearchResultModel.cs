using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models.Search
{
    public class SearchResultModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int PostType { get; set; }
        public DateTime CreationDate { get; set; }
        public int AcceptedAnswerId { get; set; }
        public decimal Rank { get; set; }
        public string Body { get; set; }
        public int ParentID { get; set; }
        public int Score { get; set; }
    }
}
