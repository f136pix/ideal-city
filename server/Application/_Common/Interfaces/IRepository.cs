using Domain.Common;

namespace Application._Common.Interfaces;

public interface IRepository<T> 
{
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
}