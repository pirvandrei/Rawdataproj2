namespace WebService.Models.Note
{
	public class NoteListModel
    {
        public string Url { get; set; } 
        public string Text { get; set; }
        public string Title { get; set; } 
        public string Type { get; set; } 
        public int PostID { get; set; } 
		public int? ParentID { get; internal set; }
    }
}
 