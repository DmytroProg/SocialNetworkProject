namespace SocialNetwork.Core.Interfaces;

public interface IRepository
{
    IQueryable<T> GetAll<T>();
}