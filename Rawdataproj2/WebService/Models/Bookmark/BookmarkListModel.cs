using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models.Bookmark
{
    public class BookmarkListModel
    {
        public string Url { get; set; }
        public int PostID { get; set; } 
		public int UserID { get; set; }
        public string Title { get; set; }
        public string Type { get; internal set; }
        public int? ParentID { get; internal set; }  
    }
}
