using Domain.Common;

namespace Application._Common.Interfaces;

public interface ICommonRepository<T> : IRepository<T>
{
    Task<T> GetByIdAsync(string id);
    T GetByIdSync(string id);
    Task<T> DeleteAsync(long id);
    public Task<T> GetByProperty(string propertyName, string value);
}