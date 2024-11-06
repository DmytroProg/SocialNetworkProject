using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Models;
using SocialNetwork.Core.Helpers;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
namespace SocialNetwork.Core.Services;
public class UserService : IUserService
{
    private readonly IRepository _repository;
    public UserService(IRepository repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<User>> GetUsers(int skip, int take) 
    {
        return await _repository.GetAll<User>()
            .Skip(skip)
            .Take(take)
            .ToArrayAsync();
    }
    public async Task<User> GetUserById(int id)
    {
        var user = await _repository.GetById<User>(id);
        if (user == null)
            throw new ArgumentException("User not found"); // TODO own types of exceptions
        return user;
    }
    public async Task<IEnumerable<User>> GetUsersByName(string name, int skip, int take)
    {
        return await _repository.GetAll<User>()
            .Where(u => u.Nickname.Contains(name))
            .Skip(skip)
            .Take(take)
            .ToArrayAsync();
    }
    public Task<User> UpdateUser(int id, User user)
    {
        if (!IsUserValid(user))
            throw new ArgumentException("User credentials aren't valid"); // TODO own types of exceptions
        user.Password = HashManager.HashCreate(user.Password);
        return _repository.Update<User>(user,id);
    }
    public Task<User> LogIn(User user)
    {
        user.IsLoggedIn = true;   
        return _repository.Update<User>(user, user.Id);
    }
    public Task LogOut(User user)
    {
        user.IsLoggedIn = false;   
        return _repository.Update<User>(user, user.Id);
    }
    public Task<User> SignUp(User user)
    {
        if (!IsUserValid(user))
            throw new ArgumentException("User credentials aren't valid"); // TODO own types of exceptions
        user.Password = HashManager.HashCreate(user.Password);
        return _repository.Add(user);
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