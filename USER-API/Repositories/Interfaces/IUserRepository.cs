using USER_API.Models;
using USER_API.Pagination;

namespace USER_API.Repositories.Interfaces;

public interface IUserRepository
{
    public Task<PagedList<User>> GetUsers(PaginationParameters parameters);
    public Task<User> GetUserById(int id);
    public Task<User> GetUserByLogin(string login);
    public Task CreateUser(User user, bool isAdmin);
    public Task<string> CheckCredentials(string login, string password);
    public void BlockUser(User user);
}