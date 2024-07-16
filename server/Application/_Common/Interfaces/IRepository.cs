using Domain.Common;

namespace Application._Common.Interfaces;

public interface IRepository<T>
{
    public abstract Task<IReadOnlyList<T>> GetAllAsync();
    public abstract Task<T> AddAsync(T entity);
    public abstract Task<T> UpdateAsync(T entity);
}