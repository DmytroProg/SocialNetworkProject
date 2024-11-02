using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Models;
using SocialNetwork.Core.Helpers;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
namespace SocialNetwork.Core.Services;
public class UserService : IUserService
{
    private readonly IRepository repository;
    public UserService(IRepository repository)
    {
        this.repository = repository;
    }
    public IEnumerable<User> GetUsers() 
    {
        return repository.GetAll<User>();
    }
    public async Task<User> GetUserById(int id)
    {
        var user = await repository.GetById<User>(id);
        if (user == null)
            throw new ArgumentException("User not found"); // TODO own types of exceptions
        return user;
    }
    public async Task<User?> GetUserByName(string name)
    {
        var users = repository.GetAll<User>();
        var user = await users.FirstOrDefaultAsync(u => u.Nickname == name);
        if (user == null)
            throw new ArgumentException("User not found"); // TODO own types of exceptions
        return user;
    }
    public async Task<User> UpdateUser(int id, User user)
    {
        if (!IsUserValid(user))
            throw new ArgumentException("User credentials aren't valid"); // TODO own types of exceptions
        user.Password = HashManager.HashCreate(user.Password);
        return await repository.Update<User>(user,id);
    }
    public async Task<User> LogIn(User user)
    {
        user.IsLoggedIn = true;   
        return await repository.Update<User>(user, user.Id);
    }
    public async Task<User> LogOut(User user)
    {
        user.IsLoggedIn = false;   
        return await repository.Update<User>(user, user.Id);
    }
    public async Task<User> SignUp(User user)
    {
        if (!IsUserValid(user))
            throw new ArgumentException("User credentials aren't valid"); // TODO own types of exceptions
        user.Password = HashManager.HashCreate(user.Password);
        return await repository.Add(user);
    }
    public async Task DeleteUser(int id)
    {
        await repository.Delete<User>(id);
    }
    #region Validation logic
    private bool IsUserValid(User user)
    {
        // Cheks if a string has at least one latin character, at least one digit and only one '_' character. Other characters should be excluded
        var isNicknameValid = new Regex(@"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d]*_[a-zA-Z\d]*$").IsMatch(user.Nickname);
        // Cheks if a string has at least one lower-case latin character, at least one upper-case latin character and at least one digit. The string must be at least 8 characters long
        var isPasswordValid = new Regex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$").IsMatch(user.Password);
        return isNicknameValid && isPasswordValid;
    }
    #endregion
}