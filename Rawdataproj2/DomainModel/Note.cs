namespace DomainModel
{
    public class Note
    {
        public string Text { get; set; }

        public int UserId { get; set; } 
        public User User { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}