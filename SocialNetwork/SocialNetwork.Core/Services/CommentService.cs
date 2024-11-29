using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Models;

namespace SocialNetwork.Core.Services;

public class CommentService : ICommentService
{
	private readonly IRepository _repository;
	public CommentService(IRepository repository)
	{
		_repository = repository;
	}

	public async Task<Comment> AddComment(Comment comment)
	{
		_ = await _repository.GetById<User>(comment.UserId);
		_ = await _repository.GetById<Post>(comment.PostId);
        if (string.IsNullOrEmpty(comment.Text))
		{
			throw new ArgumentException("Comment can't be empty");
		}
		comment.CreatedAt = DateTime.UtcNow;
		
		return await _repository.Add(comment);
	}

	public async Task<Comment> GetComment(int id)
	{
		var comment = await _repository.GetById<Comment>(id);

		if (comment == null)
		{
			throw new KeyNotFoundException($"Comment with ID {id} not found.");
		}

		return comment;
	}

	public async Task<IEnumerable<Comment>> GetComments(int postId)
	{
		var comments = await _repository.GetAll<Comment>().Where(c => c.PostId == postId).ToArrayAsync();

		return comments;
	}

}
