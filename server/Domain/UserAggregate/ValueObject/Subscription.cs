using Domain.Common;

namespace Domain.User.ValueObject;

public class Subscription : Common.ValueObject
{
    public SubscriptionType SubscriptionType { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public bool IsActive { get; private set; } = false;

    public static Subscription Create()
    {
        return new Subscription(SubscriptionType.Basic, DateTime.Now.AddMonths(6));
    }

    private Subscription(SubscriptionType subscriptionType, DateTime expirationDate)
    {
        SubscriptionType = subscriptionType;
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
        return SubscriptionType.GetMaxPostsAccessed();
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return SubscriptionType;
        yield return ExpirationDate;
        yield return IsActive;
        
    }
}