namespace SocialNetwork.Core.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Media { get; set; }
        public long LikesCount { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
