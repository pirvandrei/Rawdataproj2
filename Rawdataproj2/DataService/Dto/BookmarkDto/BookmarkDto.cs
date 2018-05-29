using System;
using System.Collections.Generic;
using System.Text;
using DomainModel;

namespace DataService.Dto.BookmarkDto
{
    public class BookmarkDto
    {
        public int PostID { get; set; }        
        public int UserID { get; set; }  
        public int? ParentID { get; set; } 
        public string Title { get; set; }
        public string Posttype { get; set; }
    }
}
