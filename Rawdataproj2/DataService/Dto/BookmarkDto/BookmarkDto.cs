using System;
using System.Collections.Generic;
using System.Text;
using DomainModel;

namespace DataService.Dto.BookmarkDto
{
    public class BookmarkDto
    {
        public int PostID { get; set; }
        public Post Post { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }
    }
}
