using SocialNetwork.Core.Models;
namespace SocialNetwork.Core.Interfaces;

internal interface ICommentServise
{
	Task<Comment> AddComment(Comment comment);
	Task<Comment> GetComment(int id);
	Task<IEnumerable<Comment>> GetComments(int postId);
}