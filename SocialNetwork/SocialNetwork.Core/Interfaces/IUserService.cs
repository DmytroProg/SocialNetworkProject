using SocialNetwork.Core.Models;
namespace SocialNetwork.Core.Interfaces;
public interface IUserService
{
    IEnumerable<User> GetUsers();
    User? GetUserById(int id);
    User? GetUserByName(string name);
    void UpdateUser(User user);
    User LogIn(User user);
    User LogOut(User user);
    User SignUp(User user);
}