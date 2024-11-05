using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Core.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        [ForeignKey (nameof(User))]
        public int UserId { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
