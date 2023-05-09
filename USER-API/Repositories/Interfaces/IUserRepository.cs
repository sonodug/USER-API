using USER_API.Models;

namespace USER_API.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetUsers();
    public Task<User> GetUserById(int id);
    public Task CreateUser(User user, bool isAdmin);
    public void BlockUser(User user);
}