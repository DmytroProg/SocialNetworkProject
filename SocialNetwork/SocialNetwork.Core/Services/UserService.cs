using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Models;
using SocialNetwork.Core.Helpers;
using System.Text.RegularExpressions;
using System;
namespace SocialNetwork.Core.Services;
public class UserService : IUserService
{
    // TODO repository
    public IEnumerable<User> GetUsers() 
    {
        return new List<User>(); // return data from repository
    }
    public User? GetUserById(int id)
    {
        var user = GetUsers().FirstOrDefault(unit => unit.Id == id);
        return user;
    }
    public User? GetUserByName(string name)
    {
        var user = GetUsers().FirstOrDefault(unit => unit.Nickname == name);
        return user;
    }
    public void UpdateUser(User user)
    {
        if (!IsUserValid(user))
            throw new ArgumentException("User credentials aren't valid"); // TODO own types of exceptions
        var users = GetUsers().ToList();
        var index = users.IndexOf(users.First(unit => unit.Id == user.Id));
        users[index].Password = HashManager.HashCreate(users[index].Password);
        users[index] = user;
    }
    public User LogIn(User user)
    {
        user.IsLoggedIn = true;   // TODO save in repository
        return user;
    }
    public User LogOut(User user)
    {
        user.IsLoggedIn = false;   // TODO save in repository
        return user;
    }
    public User SignUp(User user)
    {
        if (!IsUserValid(user))
            throw new Exception("User credentials aren't valid"); // TODO own types of exceptions
        user.Password = HashManager.HashCreate(user.Password);
        GetUsers().ToList().Add(user);
        return user;
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