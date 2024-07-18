using System.Collections.ObjectModel;
using Domain.Common;

namespace Domain.User.ValueObject;

public class Subscription : Entity<SubscriptionId>
{
    private readonly List<UserAggregate.User> _users = new();
    // public SubscriptionType SubscriptionType { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public bool IsActive { get; private set; } = false;
    public ReadOnlyCollection<UserAggregate.User> Users => _users.AsReadOnly();

    public static Subscription Create()
    {
        return new Subscription(SubscriptionType.Basic, DateTime.Now.AddMonths(6));
    }

    private Subscription(SubscriptionType subscriptionType, DateTime expirationDate)
    {
        // SubscriptionType = subscriptionType;
        ExpirationDate = expirationDate;
    }

    public void ActivateSubscription()
    {
        IsActive = true;
    }

    public void DeactivateSubscription()
    {
        IsActive = false;
    }

    public int GetMaxPostsAccessed()
    {
        return 5;
        // return SubscriptionType.GetMaxPostsAccessed();
    }
#pragma warning disable CS8618
    public Subscription()
    {
    }
#pragma warning restore CS8618
}