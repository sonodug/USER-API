namespace USER_API.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}