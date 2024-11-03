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
        public Task<IEnumerable<Post>> GetPosts(bool isNiknameFiltered, int skip, int take);
        public Task<Post> CreatePost(Post post);
        public Task<Post> GetPostById(int id);
    }
}
