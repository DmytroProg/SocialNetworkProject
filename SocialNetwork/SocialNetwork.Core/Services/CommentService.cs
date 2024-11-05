using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Models;

namespace SocialNetwork.Core.Services;

public class CommentService : ICommentServise
{
    private readonly IRepository _repository;
    public CommentService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Comment> AddComment(Comment comment)
    {
        if (comment == null)
        {
            throw new ArgumentNullException(nameof(comment), "Comment can't be empty");
        }
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

}


