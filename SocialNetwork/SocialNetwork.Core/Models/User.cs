namespace SocialNetwork.Core.Models;

public class User
{
    public int Id { get; set; }
    public string Nickname { get; set; }
    public string? Password { get; set; }
    public string? PhoneNumber { get; set; }
    public bool IsLoggedIn { get; set; }
    public string? UserDescription { get; set; }
    public string? UserIconFileName { get; set; }
    public List<Post>? Posts { get; set; }
    public DateTime WasCreated { get; set; }
    public DateTime LastSignedIn { get; set; }
}