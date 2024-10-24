using SocialNetwork.Core.Models;

namespace SocialNetwork.Core.Interfaces;

// Single Responsibility

public interface IUserService
{
    // pagination
    IEnumerable<User> GetAllUsers(int skip = 0, int take = 20);

    Task<User> AddUser(User user);
    Task<User> UpdateUser(User user);
    Task DeleteUser(int id);
}