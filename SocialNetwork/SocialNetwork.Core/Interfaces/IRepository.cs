namespace SocialNetwork.Core.Interfaces;

public interface IRepository
{
    IQueryable<T> GetAll<T>() where T : class;
    public Task<T> Add<T>(T entity) where T : class;
    public Task<T> Update<T>(T entity, int id) where T: class;
    public Task<T> GetById<T>(int id) where T : class;
    public Task Delete<T>(int id) where T : class;
}