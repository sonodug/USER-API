using Microsoft.EntityFrameworkCore;
using USER_API.Context;
using USER_API.Models;
using USER_API.Repositories.Interfaces;

namespace USER_API.Repositories.Implimentations;

public class UserRepository : BaseRepository, IUserRepository
{
    public UserRepository(DatabaseContext context) : base(context)
    {
        
    }
    
    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _context.Users
            .Include(g => g.UserGroup)
            .Include(s => s.UserState)
            .ToListAsync();
    }

    public async Task<User> GetUser(int id)
    {
        return await _context.Users
            .Include(g => g.UserGroup)
            .Include(s => s.UserState)
            .FirstOrDefaultAsync(u => u.Id.Equals(id));
    }

    public async Task<User> CreateUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateUser(User user)
    {
        var userToUpdate = _context.Users.FirstOrDefault(u => u.Id.Equals(user.Id));

        if (userToUpdate != null)
            _context.Update(userToUpdate);

        await _context.SaveChangesAsync();
        return userToUpdate;
    }

    public async Task DeleteUser(User user)
    {
        var userToDelete = _context.Users.FirstOrDefault(u => u.Id == user.Id);
        if (userToDelete != null)
        {
            userToDelete.UserState.Code = "Blocked";
        }

        await _context.SaveChangesAsync();
    }
}