namespace DomainModel
{
    public class Note
    {
        public string Text { get; set; }

        public int UserID { get; set; } 
        public User User { get; set; }

        public int PostID { get; set; }
        public Post Post { get; set; }
    }
}