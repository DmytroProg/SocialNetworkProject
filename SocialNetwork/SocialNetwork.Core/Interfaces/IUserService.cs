using SocialNetwork.Core.Models;
namespace SocialNetwork.Core.Interfaces;
public interface IUserService
{
    Task<IEnumerable<User>> GetUsers(int skip, int take);
    Task<User> GetUserById(int id);
    Task<IEnumerable<User>> GetUsersByName(string name, int skip, int take);
    Task<User> UpdateUser(int id, User user);
    Task<User> LogIn(User user);
    Task LogOut(User user);
    Task<User> SignUp(User user);
}