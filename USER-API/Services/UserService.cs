using USER_API.Models;
using USER_API.Pagination;
using USER_API.Repositories;
using USER_API.Repositories.Interfaces;
using USER_API.Repositories.Implimentations;
using USER_API.AuxiliaryModels;
using USER_API.Services.Communication;

namespace USER_API.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IProcessUnit _processUnit;

    public UserService(IUserRepository userRepository, IProcessUnit processUnit)
    {
        _userRepository = userRepository;
        _processUnit = processUnit;
    }
    
    public async Task<PagedList<User>> GetAsync(PaginationParameters parameters)
    {
        return await _userRepository.GetUsers(parameters);
    }

    public async Task<User> GetByLoginAsync(string login)
    {
        return await _userRepository.GetUserByLogin(login);
    }
    
    public async Task<User> GetByIdAsync(int id)
    {
        return await _userRepository.GetUserById(id);
    }

    public async Task<ControlUserResponse> RegisterAsync(User user, bool isAdmin = false)
    {
        try
        {
            await _userRepository.CreateUser(user, isAdmin);
            await _processUnit.CompleteAsync();
            return new ControlUserResponse(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return new ControlUserResponse($"An error occurred when creating the user: {ex.Message} {ex.InnerException.Message}");
        }
    }
    
    public async Task<ControlUserResponse> BlockAsync(int id)
    {
        var registeredUser = await _userRepository.GetUserById(id);

        if (registeredUser == null)
            return new ControlUserResponse("User not found.");

        try
        {
            _userRepository.BlockUser(registeredUser);
            await _processUnit.CompleteAsync();

            return new ControlUserResponse(registeredUser);
        }
        catch (Exception ex)
        {
            return new ControlUserResponse($"An error occurred when deleting the user: {ex.Message}, {ex.InnerException}");
        }
    }

    public async Task<string> Authenticate(string login, string password)
    {
        return await _userRepository.CheckCredentials(login, password);
    }
}