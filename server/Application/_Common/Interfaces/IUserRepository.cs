using Domain.User;
using Domain.UserAggregate;

namespace Application._Common.Interfaces;

public interface IUserRepository : IRepository<User>
{
    public abstract Task<User> GetByIdAsync(UserId id);
    public abstract User GetByIdSync(UserId id);
    public abstract Task<User> DeleteAsync(UserId id);
    public abstract Task<User> GetByProperty(string propertyName, string value);
}