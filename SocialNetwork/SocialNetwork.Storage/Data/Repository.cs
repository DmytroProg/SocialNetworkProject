using SocialNetwork.Core.Interfaces;

namespace SocialNetwork.Storage.Data;

public class Repository : IRepository
{
    private readonly SocialNetworkContext _socialNetworkContext;

    public Repository(SocialNetworkContext socialNetworkContext) 
    {
        _socialNetworkContext = socialNetworkContext;
    }

    public IQueryable<T> GetAll<T>() where T : class
    {
        return _socialNetworkContext.Set<T>();
    }

    public async Task<T> Add<T>(T entity) where T : class 
    {
        _socialNetworkContext.Add(entity);
        await _socialNetworkContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> Update<T>(T new_data, int id) where T : class
    {
        var entity = await GetById<T>(id);
        _socialNetworkContext.Entry(entity).CurrentValues.SetValues(new_data);
        await _socialNetworkContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> GetById<T>(int id) where T : class
    {
        var entity = await _socialNetworkContext.Set<T>().FindAsync(id);

        if (entity == null)
        {
            throw new Exception($"Entity with ID: {id} not found.");
        }

        return entity;
    }

    public async Task Delete<T>(int id) where T : class 
    {
        var entity = await GetById<T>(id);
        _socialNetworkContext.Set<T>().Remove(entity);
        await _socialNetworkContext.SaveChangesAsync();
    }
}