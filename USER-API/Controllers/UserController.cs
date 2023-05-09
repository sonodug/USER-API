/*using Microsoft.AspNetCore.Mvc;
using USER_API.Models;
using USER_API.Repositories.Interfaces;

namespace USER_API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _users;

    public UserController(IUserRepository users)
    {
        _users = users;
    }

    [HttpGet]
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _users.GetUsers();
    }
    
    [HttpGet("{id}")]
    public async Task<User> Get(Guid id)
    {
        return await _users.GetUser(id);
    }

    [HttpPost]
    public async Task<User> Post(User user)
    {
        return await _users.CreateUser(user);
    }
    
    [HttpPut]
    public async Task<User> Put(User user)
    {
        
    }
}*/