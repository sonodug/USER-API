using USER_API.Models;
using USER_API.Pagination;
using USER_API.AuxiliaryModels;
using USER_API.Services.Communication;

namespace USER_API.Services;

public interface IUserService
{
    Task<PagedList<User>> GetAsync(PaginationParameters parameters);
    Task<User> GetByIdAsync(int id);
    Task<User> GetByLoginAsync(string login);
    Task<ControlUserResponse> RegisterAsync(User user, bool isAdmin);
    Task<ControlUserResponse> BlockAsync(int id);
    Task<string> Authenticate(string login, string password);
}