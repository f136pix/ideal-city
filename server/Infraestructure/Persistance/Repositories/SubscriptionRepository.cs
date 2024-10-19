using Application._Common.Interfaces;
using Domain.User.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistance.Repositories;

public class SubscriptionRepository : ISubscriptionRepository
{
    private readonly IdealCityDbContext _context;

    public SubscriptionRepository(IdealCityDbContext context)
    {
        _context = context;
    }
    
    public Task<IReadOnlyList<Subscription>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<Subscription> AddAsync(Subscription entity)
    {
        var subscription = await _context.AddAsync(entity);
        return subscription.Entity;
    }

    public Task<Subscription> UpdateAsync(Subscription entity)
    {
        var subscription = _context.Update(entity);
        return Task.FromResult(subscription.Entity);
    }

    public Task<Subscription> GetByIdAsync(SubscriptionId id)
    {
        return _context.Subscriptions
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public Subscription GetByIdSync(SubscriptionId id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Subscription subscription)
    {
        _context.Subscriptions.Remove(subscription);
        return Task.CompletedTask;
    }

    public Task<Subscription> GetByProperty(string propertyName, string value)
    {
        throw new NotImplementedException();
    }
}