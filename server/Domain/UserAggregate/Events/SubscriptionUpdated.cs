using Domain.Common;
using Domain.User.ValueObject;

namespace Domain.UserAggregate.Events;



public record SubscriptionUpdated(Subscription subscription) : IDomainEvent;