using SocialNetwork.Core.Interfaces;

namespace SocialNetwork.Storage;

public class Repository : IRepository
{
    public IQueryable<T> GetAll<T>()
    {
        // return data from database
        throw new NotImplementedException();
    }
}