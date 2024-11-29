using SocialNetwork.Core.Helpers;

namespace SocialNetwork.Core.Models;

public class User
{
    public int Id { get; set; }
    public string Nickname { get; set; }
    public string Password { get; set; }
    public string? PhoneNumber { get; set; }
    public OnlineStatus OnlineStatus { get; set; }
    public bool IsLoggedIn { get; set; }
    public string? UserDescription { get; set; }
    public string? UserIconFileName { get; set; }
    public ICollection<Post>? Posts { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastSignedIn { get; set; }
}