namespace BlogApi.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleImage { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool Visible { get; set; }
    }
}
