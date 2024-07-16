namespace Application._Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync();
}