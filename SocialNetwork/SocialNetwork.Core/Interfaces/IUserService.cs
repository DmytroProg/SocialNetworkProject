using SocialNetwork.Core.Models;
namespace SocialNetwork.Core.Interfaces;
public interface IUserService
{
    IEnumerable<User> GetUsers();
    Task<User> GetUserById(int id);
    Task<User?> GetUserByName(string name);
    Task<User> UpdateUser(int id, User user);
    Task<User> LogIn(User user);
    Task<User> LogOut(User user);
    Task<User> SignUp(User user);
}