using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Models;

namespace SocialNetwork.Core.Services
{
    internal class CommentService : ICommentService
    {
        public Task<Comment> AddComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> GetComment(int id)
        {
            throw new NotImplementedException();
        }
    }
}
