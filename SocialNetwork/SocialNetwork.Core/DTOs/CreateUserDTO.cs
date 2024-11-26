using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.DTOs
{
    public class CreateUserDTO
    {
        public string Nickname { get; set; }
        public string Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserDescription { get; set; }
        public string? UserIconFileName { get; set; }
    }
}
