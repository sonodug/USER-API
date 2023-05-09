using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using USER_API.Models;
using USER_API.Repositories.Interfaces;
using USER_API.Resources;
using USER_API.Services;

namespace USER_API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<UserResource>> GetAllAsync()
    {
        var users = await _userService.ListAsync();
        var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
        return resources;
    }
    
    // [HttpGet("{id}")]
    // public async Task<User> Get(int id)
    // {
    //     return await _users.GetUser(id);
    // }

    /*[HttpPost]
    public async Task<User> Post(User user)
    {
        return await _users.CreateUser(user);
    }
    
    [HttpPut]
    public async Task<User> Put(User user)
    {
        
    }*/
}