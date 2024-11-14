using SocialNetwork.Core.Models;

namespace SocialNetwork.Tests;

public class PostComparer : IEqualityComparer<Post>
{
    public bool Equals(Post x, Post y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Id == y.Id && x.Description == y.Description && x.Media == y.Media && x.LikesCount == y.LikesCount;
    }

    public int GetHashCode(Post obj)
    {
        return HashCode.Combine(obj.Id, obj.Description, obj.Media, obj.LikesCount);
    }
}