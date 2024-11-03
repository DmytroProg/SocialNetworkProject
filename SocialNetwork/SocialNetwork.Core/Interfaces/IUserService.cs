using SocialNetwork.Core.Models;
namespace SocialNetwork.Core.Interfaces;
public interface IUserService
{
    Task<IEnumerable<User>> GetUsers(int skip, int take);
    Task<User> GetUserById(int id);
    Task<User?> GetUserByName(string name);
    Task<User> UpdateUser(int id, User user);
    Task<User> LogIn(int id);
    Task LogOut(int id);
    Task<User> SignUp(User user);
}