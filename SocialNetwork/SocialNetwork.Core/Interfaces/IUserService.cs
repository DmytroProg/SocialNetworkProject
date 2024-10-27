using SocialNetwork.Core.Models;
namespace SocialNetwork.Core.Interfaces;
public interface IUserService
{
    List<User> Users { get; set; }
    List<User> GetUsers();
    User? GetUserById(int id);
    User? GetUserByName(string name);
    void UpdateUser(User user);
    void LogIn(User user);
    void LogOut(User user);
    void SignUp(User user);
}