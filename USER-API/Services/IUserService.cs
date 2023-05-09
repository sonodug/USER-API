using USER_API.Models;
using USER_API.Resources;
using USER_API.Services.Communication;

namespace USER_API.Services;

public interface IUserService
{
    Task<IEnumerable<User>> GetAsync();
    Task<User> GetByIdAsync(int id);
    Task<RegisterUserResponse> RegisterAsync(User user, bool isAdmin);
}