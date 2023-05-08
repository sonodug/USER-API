using USER_API.Context;
using USER_API.Models;
using USER_API.Repositories.Interfaces;

namespace USER_API.Repositories.Implimentations;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _context;
    
    public IEnumerable<User> GetUsers()
    {
        return _context.Users;
    }

    public User GetUser(Guid id)
    {
        return _context.Users.FirstOrDefault(u => u.Id.Equals(id));
    }

    public User CreateUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
        return user;
    }

    public User UpdateUser(User user)
    {
        var userToUpdate = _context.Users.FirstOrDefault(u => u.Id.Equals(user.Id));
        if (userToUpdate != null)
        {
            _context.Update(userToUpdate);
        }
        
        _context.SaveChanges();
        return userToUpdate;
    }

    public void DeleteUser(User user)
    {
        var userToDelete = _context.Users.FirstOrDefault(u => u.Id == user.Id);
        if (userToDelete != null)
        {
            userToDelete.UserState.Code = "Blocked";
        }

        _context.SaveChanges();
    }
}