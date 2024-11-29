using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Models;
using SocialNetwork.Core.Helpers;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
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
    public async Task<User> GetUserByName(string name)
    {
        var user = await _repository.GetAll<User>()
            .SingleOrDefaultAsync(u => u.Nickname.Equals(name));
        if (user == null)
        {
            throw new ArgumentException("User not found");
        }
        return user;
    }
    public async Task<bool> IsNicknameUnique(string name)
    {
        var user = await _repository.GetAll<User>()
            .SingleOrDefaultAsync(u => u.Nickname.Equals(name));
        return user == null;
    }
    public async Task<IEnumerable<User>> GetUsersByName(string name, int skip, int take)
    {
        return await _repository.GetAll<User>()
            .Where(u => u.Nickname.Contains(name))
            .Skip(skip)
            .Take(take)
            .ToArrayAsync();
    }
    public async Task<User> UpdateUser(int id, User user)
    {
        if (! await IsUserValid(user, false))
            throw new ArgumentException("User credentials aren't valid"); // TODO own types of exceptions
        return await _repository.Update<User>(user,id);
    }
    public async Task<User> LogIn(string nickname, string password)
    {
        var targetUser = await GetUserByName(nickname);
        if (!HashManager.HashCompare(password, targetUser.CreatedAt, targetUser.Password))
        {
            throw new ArgumentException("Wrong password");
        }    
        targetUser.UserOnlineStatus = OnlineStatus.Online;
        targetUser.LastLoggedIn = DateTime.UtcNow;
        return await _repository.Update<User>(targetUser, targetUser.Id);
    }
    public async Task LogOut(int id)
    {
        var targetUser = await _repository.GetById<User>(id);
        if (targetUser == null)
            throw new ArgumentException("User not found");
        targetUser.UserOnlineStatus = OnlineStatus.Offline;
        targetUser.LastLoggedIn = DateTime.UtcNow;
        await _repository.Update<User>(targetUser, id);
    }
    public async Task<User> SignUp(User user)
    {
        if (! await IsUserValid(user, true))
        {
            throw new ArgumentException("User credentials aren't valid"); // TODO own types of exceptions
        }
        user.CreatedAt = DateTime.UtcNow;
        user.LastLoggedIn = user.CreatedAt;
        user.Password = HashManager.HashCreate(user.Password, user.CreatedAt);
        return await _repository.Add(user);
    }
    #region Validation logic
    private async Task<bool> IsUserValid(User user, bool isCreating)
    {
        // Checks if a string has at least one latin character, digit or '_' character. Other characters should be excluded
        var isNicknameValid = new Regex(@"^[a-zA-Z0-9_]+$").IsMatch(user.Nickname);
        // Checks if a string has at least one lower-case latin character, at least one upper-case latin character and at least one digit. The string must be at least 8 characters long
        var isPasswordValid = new Regex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$").IsMatch(user.Password);

        if (isCreating && !await IsNicknameUnique(user.Nickname))
        {
            throw new ArgumentException("Nickname already claimed");
        }
        return isNicknameValid && isPasswordValid;
    }
    #endregion
}