using USER_API.Models;
using USER_API.Repositories;
using USER_API.Repositories.Interfaces;
using USER_API.Resources;
using USER_API.Services.Communication;

namespace USER_API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<User>> GetAsync()
    {
        return await _userRepository.GetUsers();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _userRepository.GetUser(id);
    }

    public async Task<RegisterUserResponse> RegisterAsync(User user, bool isAdmin = false)
    {
        try
        {
            await _userRepository.CreateUser(user, isAdmin);
            await _unitOfWork.CompleteAsync();
            return new RegisterUserResponse(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new RegisterUserResponse($"An error occurred when creating the user: {ex.InnerException.Message}");
        }
    }
}