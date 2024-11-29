using SocialNetwork.Core.Helpers;
using SocialNetwork.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string? PhoneNumber { get; set; }
        public OnlineStatus UserOnlineStatus { get; set; }
        public string? UserDescription { get; set; }
        public string? UserIconFileName { get; set; }
    }
}
