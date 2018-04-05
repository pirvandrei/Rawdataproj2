using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Models.Note
{
    public class UpdateNoteModel
    {
        public string Text { get; set; }
        public int UserID { get; set; }
        public int PostID { get; set; }
    }
}
