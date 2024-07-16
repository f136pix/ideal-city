using Application._Common.Interfaces;
using Domain.User;
using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistance.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IdealCityDbContext _dbContext;

    public UserRepository(IdealCityDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<IReadOnlyList<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User> AddAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<User> UpdateAsync(User entity)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync(User id)
    {
        throw new NotImplementedException();
    }

    public User GetByIdSync(UserId id)
    {
        throw new NotImplementedException();
    }

    public Task<User> DeleteAsync(UserId id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByProperty(string propertyName, string value)
    {
        throw new NotImplementedException();
        // switch (propertyName)
        // {
        //     case "Email":
        //         return _dbContext.Users
        //             .FirstOrDefaultAsync(u => u.Email == value);
        //
        //     default:
        //         throw new ArgumentException("Invalid property name");
        // }
    }
}