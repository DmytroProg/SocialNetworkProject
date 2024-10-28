using SocialNetwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Interfaces
{
    public interface IPostService
    {
        public List<Post> Posts { get; set; }
        public void CreatePost(Post post);
        public List<Post> GetPosts();
        public Post? GetPostById(int id);
    }
}
