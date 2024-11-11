using SocialNetwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Media { get; set; }
        public long LikesCount { get; set; }
        public int UserId { get; set; }
    }
}
