using SocialNetwork.Core.Models;

namespace SocialNetwork.Tests;

public class UserComparer : IEqualityComparer<User>
{
    public bool Equals(User x, User y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Id == y.Id && x.Nickname == y.Nickname && x.Password == y.Password && x.PhoneNumber == y.PhoneNumber && x.UserOnlineStatus.status == y.UserOnlineStatus.status && x.UserDescription == y.UserDescription && x.UserIconFileName == y.UserIconFileName && x.UserOnlineStatus.LastLoggedIn.Equals(y.UserOnlineStatus.LastLoggedIn);
    }

    public int GetHashCode(User obj)
    {
        var hashCode = new HashCode();
        hashCode.Add(obj.Id);
        hashCode.Add(obj.Nickname);
        hashCode.Add(obj.Password);
        hashCode.Add(obj.PhoneNumber);
        hashCode.Add(obj.UserOnlineStatus.status);
        hashCode.Add(obj.UserDescription);
        hashCode.Add(obj.UserIconFileName);
        hashCode.Add(obj.CreatedAt);
        hashCode.Add(obj.UserOnlineStatus.LastLoggedIn);
        return hashCode.ToHashCode();
    }
}