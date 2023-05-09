using USER_API.Models;

namespace USER_API.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetUsers();
    public Task<User> GetUser(int id);
    public Task<User> CreateUser(User user);
    public Task<User?> UpdateUser(User user);
    public Task DeleteUser(User user);
}