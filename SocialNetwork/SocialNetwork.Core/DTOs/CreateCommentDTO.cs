namespace SocialNetwork.Core.DTOs
{
    public class CreateCommentDTO
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; }
    }
}
