using System;
using System.Collections.Generic;
using System.Linq;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Models;
namespace SocialNetwork.Core.Services
{
    public class PostService : IPostService
    {
        // TODO repository
        public List<Post> Posts { get; set; }
        public PostService()
        {
            Posts = new List<Post>();
        }
        public void CreatePost(Post post)
        {
            if (post.Description == null || post.LikesCount < 0)
                throw new Exception("Invalid post build");  // TODO own types of exceptions
            Posts.Add(post);
        }
        public Post? GetPostById(int id)
        {
            Post? post = Posts.FirstOrDefault(unit => unit.Id == id);
            return post;
        }
        public List<Post> GetPosts()
        {
            return Posts;
        }
    }
}
