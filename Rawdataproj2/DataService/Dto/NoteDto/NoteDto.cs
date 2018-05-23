using DomainModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Dto.NoteDto
{
    public class NoteDto
    {
        public string Text { get; set; }

        public int UserID { get; set; }
        public int PostID { get; set; }
    }
}
