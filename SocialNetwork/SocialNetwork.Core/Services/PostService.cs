using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Models;
namespace SocialNetwork.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IRepository repository;
        public PostService(IRepository repository)
        {
            this.repository = repository;
        }
        public IEnumerable<Post> GetPosts()
        {
            return repository.GetAll<Post>();
        }
        public async Task<Post> CreatePost(Post post)
        {
            if (post.Description == null || post.LikesCount < 0)
                throw new ArgumentException("Invalid post build");  // TODO own types of exceptions
            return await repository.Add(post);
        }
        public async Task<Post> GetPostById(int id)
        {
            Post? post = await repository.GetById<Post>(id);
            if (post == null)
                throw new ArgumentException("Post not found");    // TODO own types of exceptions
            return post;
        }
    }
}
