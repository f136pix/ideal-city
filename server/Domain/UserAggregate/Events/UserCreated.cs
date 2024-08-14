using Domain.Common;

namespace Domain.UserAggregate.Events;

public record UserCreated(User user) : IDomainEvent;