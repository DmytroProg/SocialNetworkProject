namespace SocialNetwork.Core.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Message { get; set; }
        public object? Media { get; set; }
        public long LikesCount { get; set; }
        public long DislikesCount { get; set; }
        public long RepostsCount { get; set; }
    }
}
