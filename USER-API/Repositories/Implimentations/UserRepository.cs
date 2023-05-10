using Microsoft.EntityFrameworkCore;
using USER_API.Context;
using USER_API.Models;
using USER_API.Pagination;
using USER_API.Repositories.Interfaces;

namespace USER_API.Repositories.Implimentations;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(DatabaseContext context) : base(context)
    {
        
    }
    
    public async Task<PagedList<User>> GetUsers(PaginationParameters parameters)
    {
        var users = await _context.Users
            .Include(g => g.UserGroup)
            .Include(s => s.UserState)
            .ToListAsync();

        var usersWithParams = new PagedList<User>(users, users.Count, parameters.PageNumber, parameters.PageSize);

        return usersWithParams;
    }

    public async Task<User> GetUserById(int id)
    {
        return await _context.Users
            .Include(g => g.UserGroup)
            .Include(s => s.UserState)
            .FirstOrDefaultAsync(u => u.Id.Equals(id));
    }
    
    public async Task<User> GetUserByLogin(string login)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Login.Equals(login));
    }

    public async Task CreateUser(User user, bool isAdmin = false)
    {
        if (isAdmin)
            user.UserGroup = _context.UserGroups.FirstOrDefault(u => u.Code == "Admin");
        else
            user.UserGroup = _context.UserGroups.FirstOrDefault(u => u.Code == "User");
        
        user.CreatedDate = DateTime.Now;
        user.UserState = _context.UserStates.FirstOrDefault(u => u.Code == "Active");
        user.UserGroupId = user.UserGroup.Id;
        user.UserStateId = user.UserState.Id;
        user.UserState.Users.Add(user);
        user.UserGroup.Users.Add(user);
        
        await _context.Users.AddAsync(user);
    }

    public async Task<string> CheckCredentials(string login, string password)
    {
        if(await Task.FromResult(_context.Users.SingleOrDefault(x => x.Login == login && x.Password == password)) != null)
        {
            return _context.Users.Include(s => s.UserState).SingleOrDefault(x => x.Login == login && x.Password == password).UserState.Code;
        }
        
        return "None";
    }
    
    public void BlockUser(User user)
    {
        user.UserState = _context.UserStates.FirstOrDefault(u => u.Code == "Blocked");
        _context.Users.Update(user);
    }

}