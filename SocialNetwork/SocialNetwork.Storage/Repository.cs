using SocialNetwork.Core.Interfaces;

namespace SocialNetwork.Storage;

public class Repository : IRepository
{
    public IQueryable<T> GetAll<T>() where T : class
    {
        throw new NotImplementedException();
    }
    public Task<T> Add<T>(T entity) where T : class
    {
        throw new NotImplementedException();
    }

    public Task Delete<T>(int id) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<T?> GetById<T>(int id) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<T> Update<T>(T entity, int id) where T : class
    {
        throw new NotImplementedException();
    }
}