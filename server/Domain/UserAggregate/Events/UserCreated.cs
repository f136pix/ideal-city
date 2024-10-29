using Domain.Common;
using Domain.User.ValueObject;

namespace Domain.UserAggregate.Events;

public record UserCreated(string Email, string Name, SubscriptionType SubscriptionType, Guid SubscriptionGuid) : IDomainEvent;