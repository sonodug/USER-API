using USER_API.Models;
using USER_API.Repositories.Interfaces;

namespace USER_API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _userRepository.GetUsers();
    }
}