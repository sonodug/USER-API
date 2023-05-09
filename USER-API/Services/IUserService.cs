using USER_API.Models;

namespace USER_API.Services;

public interface IUserService
{
    Task<IEnumerable<User>> ListAsync();
}