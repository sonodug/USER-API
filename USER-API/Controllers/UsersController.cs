using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using USER_API.Extensions;
using USER_API.Models;
using USER_API.Repositories.Interfaces;
using USER_API.Resources;
using USER_API.Services;

namespace USER_API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private static readonly object _lock = new object();
    
    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<UserResource>> GetAll()
    {
        var users = await _userService.GetAsync();
        var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<UserResource> Get(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        var resource = _mapper.Map<User, UserResource>(user);
        return resource;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] RegisterUserResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var user = _mapper.Map<RegisterUserResource, User>(resource);
        
        var existingUser = await _userService.GetByLoginAsync(user.Login);
        
        if (existingUser != null)
            return BadRequest("User with this login already exists");
        
        var resultResponse = await _userService.RegisterAsync(user, resource.IsAdmin);
        if (!resultResponse.Succes)
            return BadRequest(resultResponse.Message);
        
        await Task.Delay(5000);
        var userResource = _mapper.Map<User, UserResource>(resultResponse.User);
        var uri = "id/" + user.Id;
        return Created(uri, userResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _userService.BlockAsync(id);

        if (!result.Succes)
            return BadRequest(result.Message);

        var userResource = _mapper.Map<User, UserResource>(result.User);
        return Ok(userResource);
    }
    
    [Authorize]
    [Route("/api/[controller]/auth")]
    [HttpGet]
    public async Task<IActionResult> Authenticate()
    {
        return Ok();
    }
}