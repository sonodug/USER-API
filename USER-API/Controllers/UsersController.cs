using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using USER_API.Extensions;
using USER_API.Models;
using USER_API.Pagination;
using USER_API.Repositories.Interfaces;
using USER_API.AuxiliaryModels;
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

    [AllowAnonymous]
    [HttpGet]
    public async Task<IEnumerable<UserAuxiliary>> GetByParams([FromQuery] PaginationParameters parameters)
    {
        var users = await _userService.GetAsync(parameters);
        var resources = _mapper.Map<IEnumerable<User>, IEnumerable<UserAuxiliary>>(users);
        
        var metadata = new
        {
            users.TotalCount,
            users.PageSize,
            users.CurrentPage,
            users.TotalPages,
            users.HasNext,
            users.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<UserAuxiliary> Get(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        var resource = _mapper.Map<User, UserAuxiliary>(user);
        return resource;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] RegisterUserAuxiliary auxiliary)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var user = _mapper.Map<RegisterUserAuxiliary, User>(auxiliary);
        
        var existingUser = await _userService.GetByLoginAsync(user.Login);
        
        if (existingUser != null)
            return BadRequest("User with this login already exists");
        
        var resultResponse = await _userService.RegisterAsync(user, auxiliary.IsAdmin);
        if (!resultResponse.Succes)
            return BadRequest(resultResponse.Message);
        
        await Task.Delay(5000);
        var userResource = _mapper.Map<User, UserAuxiliary>(resultResponse.User);
        var uri = "id/" + user.Id;
        return Created(uri, userResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _userService.BlockAsync(id);

        if (!result.Succes)
            return BadRequest(result.Message);

        var userResource = _mapper.Map<User, UserAuxiliary>(result.User);
        return Ok(userResource);
    }
    
    [Route("/api/[controller]/auth")]
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Authenticate()
    {
        return Ok();
    }
}