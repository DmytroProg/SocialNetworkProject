using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Models;
using System.Text.RegularExpressions;
using System.Xml.Linq;
namespace SocialNetwork.Core.Services;
public class UserService : IUserService
{
    // TODO repository
    public List<User> Users {get; set; }
    public UserService()
    {
        Users = new List<User>();
    }
    public List<User> GetUsers() 
    {
        return Users;
    }
    public User? GetUserById(int id)
    {
        User? user = Users.FirstOrDefault(unit => unit.Id == id);
        return user;
    }
    public User? GetUserByName(string name)
    {
        User? user = Users.FirstOrDefault(unit => unit.Nickname == name);
        return user;
    }
    public void UpdateUser(User user)
    {
        if (!UserIsValid(user))
            throw new Exception("User credentials aren't valid"); // TODO own types of exceptions
        var index = Users.IndexOf(Users.First(unit => unit.Id == user.Id));
        Users[index] = user;
    }
    public void LogIn(User user)
    {
        user.IsLoggedIn = true;
    }
    public void LogOut(User user)
    {
        user.IsLoggedIn = false;
    }
    public void SignUp(User user)
    {
        if (!UserIsValid(user))
            throw new Exception("User credentials aren't valid"); // TODO own types of exceptions
        Users.Add(user);
    }
    // Validation logic **************************************************************************************************************************
    private bool UserIsValid(User user)
    {
        // Cheks if a string has at least one latin character, at least one digit and only one '_' character. Other characters should be excluded
        var nick_name_validation = DataIsValid(user.Nickname, @"^(?=.*[a-zA-Z])(?=.*\d)[a-zA-Z\d]*_[a-zA-Z\d]*$");
        // Cheks if a string has at least one lower-case latin character, at least one upper-case latin character and at least one digit. The string must be at least 8 characters long
        var password_validation = DataIsValid(user.Password, @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$");
        return nick_name_validation && password_validation;
    }
    private bool DataIsValid(string data, string pattern)
    {
        var regex = new Regex(pattern);
        return regex.IsMatch(data);
    }
}