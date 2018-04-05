namespace WebService.Models.Note
{
    public class CreateNoteModel
    {
        public string Text { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
    }
}