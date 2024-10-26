namespace SocialNetwork.Core.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public object? Media { get; set; }
        public long LikesCount { get; set; }
        public long DislikesCount { get; set; }
        public long RepostsCount { get; set; }
    }
}
