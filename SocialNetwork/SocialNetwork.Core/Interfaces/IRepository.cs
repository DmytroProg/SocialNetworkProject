namespace SocialNetwork.Core.Interfaces;

public interface IRepository
{
    IQueryable<T> GetAll<T>() where T : class;
    public void Add<T>(T entity) where T : class;
    public void Update<T>(T entity, int id) where T: class;
    public T GetById<T>(int id) where T : class;
    public void Delete<T>(int id) where T : class;
}