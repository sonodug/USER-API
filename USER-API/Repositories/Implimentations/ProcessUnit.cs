using USER_API.Context;
using USER_API.Repositories.Interfaces;

namespace USER_API.Repositories.Implimentations;

public class ProcessUnit : IProcessUnit
{
    private readonly DatabaseContext _context;

    public ProcessUnit(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }
}