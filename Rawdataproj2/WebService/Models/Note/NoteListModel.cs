using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models.Note
{
    public class NoteListModel
    {
        public string Url { get; set; }
        public string Text { get; set; } 

        
        public int PostID { get;   set; }
        public string Title { get; set; }

        public int UserID { get; set; }
        public string UserName { get;   set; }
    }
}
