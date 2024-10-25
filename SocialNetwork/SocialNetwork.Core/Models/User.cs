namespace SocialNetwork.Core.Models;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string? Password { get; set; }
    public List<Post>? Posts { get; set; }
}