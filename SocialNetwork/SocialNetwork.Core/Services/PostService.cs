using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Models;
using Microsoft.EntityFrameworkCore;
namespace SocialNetwork.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IRepository _repository;
        public PostService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Post>> GetPosts(bool isNiknameFiltered, int skip, int take)
        {
            if(!isNiknameFiltered)
                return await _repository.GetAll<Post>().
                    OrderBy(p => p.LikesCount).
                    Skip(skip).
                    Take(take).
                    ToListAsync();
            return await _repository.GetAll<Post>().
                Include(p => p.User).
                OrderBy(p => p.User.Nickname).
                Skip(skip).
                Take(take).
                ToListAsync();
        }
        public Task<Post> CreatePost(Post post)
        {
            if (post.Description == null || post.LikesCount < 0)
                throw new ArgumentException("Invalid post build");  // TODO own types of exceptions
            return _repository.Add(post);
        }
        public async Task<Post> GetPostById(int id)
        {
            var post = await _repository.GetById<Post>(id);
            if (post == null)
                throw new ArgumentException("Post not found");    // TODO own types of exceptions
            return post;
        }
    }
}
