using USER_API.Context;

namespace USER_API.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DatabaseContext _context;

    public UnitOfWork(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}