using USER_API.Models;

namespace USER_API.Repositories.Interfaces;

public interface IUserRepository
{
    public IEnumerable<User> GetUsers();
    public User GetUser(Guid id);
    public User CreateUser(User user);
    public User UpdateUser(User user);
    public void DeleteUser(User user);

}