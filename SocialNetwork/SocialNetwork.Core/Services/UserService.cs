using SocialNetwork.Core.Interfaces;
using SocialNetwork.Core.Models;

namespace SocialNetwork.Core.Services;

public class UserService : IUserService
{
    private readonly IRepository _repository;

    public UserService(IRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<User> GetAllUsers(int skip = 0, int take = 20)
    {
        return _repository.GetAll<User>()
            .Skip(skip)
            .Take(take);
    }

    public async Task<User> AddUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUser(int id)
    {
        throw new NotImplementedException();
    }
}