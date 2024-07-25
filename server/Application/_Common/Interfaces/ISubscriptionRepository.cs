using Domain.User.ValueObject;

namespace Application._Common.Interfaces;

public interface ISubscriptionRepository : IRepository<Subscription>
{
     Task<Subscription> GetByIdAsync(SubscriptionId id);
     Subscription GetByIdSync(SubscriptionId id);
     Task<Subscription> DeleteAsync(SubscriptionId id);
     public Task<Subscription> GetByProperty(string propertyName, string value);   
}