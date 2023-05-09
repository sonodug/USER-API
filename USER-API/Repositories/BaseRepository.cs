using USER_API.Context;

namespace USER_API.Repositories;

public abstract class BaseRepository
{
    protected readonly DatabaseContext _context;

    protected BaseRepository(DatabaseContext context)
    {
        _context = context;
    }
}