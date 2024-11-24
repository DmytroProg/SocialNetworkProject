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
        if (!IsUserValid(user, true))
            throw new ArgumentException("User credentials aren't valid"); // TODO own types of exceptions
        user.Id = id;
        return _repository.Update<User>(user);
    }
    public async Task<User> LogIn(int id)
    {
        var targetUser = await _repository.GetById<User>(id);
        if (targetUser == null)
            throw new ArgumentException("User not found");
        targetUser.IsLoggedIn = true;
        return await _repository.Update<User>(targetUser);
    }
    public async Task LogOut(int id)
    {
        var targetUser = await _repository.GetById<User>(id);
        if (targetUser == null)
            throw new ArgumentException("User not found");
        targetUser.IsLoggedIn = false;
        await _repository.Update<User>(targetUser);
    }
    public Task<User> SignUp(User user)
    {
        if (!IsUserValid(user, false))
            throw new ArgumentException("User credentials aren't valid"); // TODO own types of exceptions
        user.Password = HashManager.HashCreate(user.Password);
        return _repository.Add(user);
    }
    #region Validation logic
    private bool IsUserValid(User user, bool isUpdateMatter)
    {
        // Checks if a string has at least one latin character, digit or '_' character. Other characters should be excluded
        var isNicknameValid = new Regex(@"^[a-zA-Z0-9_]+$").IsMatch(user.Nickname);
        // Checks if a string has at least one lower-case latin character, at least one upper-case latin character and at least one digit. The string must be at least 8 characters long
        var isPasswordValid = isUpdateMatter || new Regex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$").IsMatch(user.Password);
        return isNicknameValid && isPasswordValid;
    }
    #endregion
}