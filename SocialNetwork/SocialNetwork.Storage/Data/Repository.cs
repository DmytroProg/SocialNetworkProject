using SocialNetwork.Core.Interfaces;

namespace SocialNetwork.API.Data;

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

    public void Add<T>(T entity) where T : class 
    {
        _socialNetworkContext.Add(entity);
        _socialNetworkContext.SaveChanges();
    }

    public void Update<T>(T entity, int id) where T : class
    {
        var Entity = GetById<T>(id);
        _socialNetworkContext.Entry(Entity).CurrentValues.SetValues(entity);
        _socialNetworkContext.SaveChanges();
    }

    public T GetById<T>(int id) where T : class
    {
        var entity = _socialNetworkContext.Set<T>().Find(id);

        if (entity == null)
        {
            throw new Exception($"Entity with ID: {id} not found.");
        }

        return entity;
    }

    public void Delete<T>(int id) where T : class 
    {
        var entity = GetById<T>(id);

        _socialNetworkContext.Set<T>().Remove(entity);

        _socialNetworkContext.SaveChanges();
    }
}