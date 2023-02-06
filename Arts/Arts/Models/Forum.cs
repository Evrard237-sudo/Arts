namespace Arts.Models
{
    public class Forum
    {
        public int Id { get; set; }
        public string? ForumComment { get; set; }
        public string? CreateAt { get; set; }
        public int IdUser { get; set; }
        public int IdItem { get; set; }
        public string? profile { get; set; }
    }
}
