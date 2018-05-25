using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models.User;

namespace WebService.Models.Note
{
    public class NoteListModel
    {
        public string Url { get; set; } 
        public string Text { get; set; }
        public string Title { get;   set; } 
        public int PostID { get;  set; } 
        public string Type { get; internal set; }
        public UserModel User { get; set; }
    }
}
