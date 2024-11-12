using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Models;

namespace SocialNetwork.Core.Services
{
    public class CommentService : ICommentService
    {
        public async Task<Comment> AddComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public async Task<Comment> GetComment(int id)
        {
            throw new NotImplementedException();
        }
    }
}
