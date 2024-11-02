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
        public IEnumerable<Post> GetPosts();
        public Task<Post> CreatePost(Post post);
        public Task<Post> GetPostById(int id);
    }
}
