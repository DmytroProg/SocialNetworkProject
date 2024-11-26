using SocialNetwork.Core.Models;

namespace SocialNetwork.Core.Interfaces
{
    public interface ICommentService
    {
        Task<Comment> AddComment(Comment comment);
        Task<Comment> GetComment(int id);
    }
}
