
using Contracts.Subscriptions;

namespace Contracts.Subsriptions;

public record CreateSubscriptionRequest(
    SubscriptionType? SubscriptionType
);