using Application._Common.Interfaces;
using Domain.User;
using Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistance.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IdealCityDbContext _context;

    public UserRepository(IdealCityDbContext context)
    {
        _context = context;
    }

    public Task<IReadOnlyList<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<User> AddAsync(User entity)
    {
        var user = await _context.Users.AddAsync(entity);
        return user.Entity;
    }

    public Task<User> UpdateAsync(User entity)
    {
        _context.Users.Update(entity);
        return Task.FromResult(entity);
    }

    public Task<User> GetByIdAsync(UserId id)
    {
        return _context.Users
            .Include(u => u.Subscription)
            .FirstOrDefaultAsync(u => u.Id == id);

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
        switch (propertyName)
        {
            case "Email":
                return _context.Users
                    .FirstOrDefaultAsync(u => u.Email == value);

            default:
                throw new ArgumentException("Invalid property name");
        }
    }
}