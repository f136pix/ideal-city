using Domain.User.ValueObject;

namespace Application._Common.Interfaces;

public interface ISubscriptionRepository : IRepository<Subscription>
{
    Task<Subscription> GetByIdAsync(SubscriptionId id);
    Subscription GetByIdSync(SubscriptionId id);
    Task DeleteAsync(Subscription id);
    public Task<Subscription> GetByProperty(string propertyName, string value);
}