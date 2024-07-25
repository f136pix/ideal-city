using System.Collections.ObjectModel;
using Domain.Common;
using ErrorOr;

namespace Domain.User.ValueObject;

public class Subscription : Entity<SubscriptionId>
{
    private readonly List<UserAggregate.User> _users = new();
    // private readonly List<UserId> _userIds = new(); // Todo: Store user ids in DB

    public SubscriptionType SubscriptionType { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public bool IsActive { get; private set; } = false;
    public IReadOnlyList<UserAggregate.User> Users => _users.AsReadOnly();


    private Subscription(SubscriptionId id, SubscriptionType subscriptionType, DateTime expirationDate) : base(id)
    {
        IsActive = true;
        SubscriptionType = subscriptionType;
        ExpirationDate = expirationDate;
    }

    public static Subscription Create()
    {
        return new Subscription(SubscriptionId.CreateUnique(), SubscriptionType.Basic, DateTime.UtcNow.AddYears(100));
    }

    public ErrorOr<Success> AddUser(UserAggregate.User user)
    {
        if (IsActive)
        {
            _users.Add(user);
            return Result.Success;
        }

        return Error.Conflict(description: "Subscription is not active");
    }


    public static ErrorOr<Subscription> Create(SubscriptionType subscriptionType)
    {
        switch (subscriptionType.Name)
        {
            case "Premium":
                return new Subscription(SubscriptionId.CreateUnique(), SubscriptionType.Premium,
                    DateTime.UtcNow.AddYears(100));
            case "Pro":
                return new Subscription(SubscriptionId.CreateUnique(), SubscriptionType.Pro,
                    DateTime.UtcNow.AddYears(1));
            case "Basic":
                return new Subscription(SubscriptionId.CreateUnique(), SubscriptionType.Basic,
                    DateTime.UtcNow.AddYears(1));
            default:
                return Error.Unexpected(description:"This subscription type does not exist");
        }
    }

    public int GetMaxPostsMade() => this.SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Basic) => 10,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        nameof(SubscriptionType.Premium) => int.MaxValue,
        _ => throw new InvalidOperationException(),
    };

    public int GetMaxLikes() => this.SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Basic) => 10,
        nameof(SubscriptionType.Pro) => int.MaxValue,
        nameof(SubscriptionType.Premium) => int.MaxValue,
        _ => throw new InvalidOperationException(),
    };

    public int GetMaxPeopleInPlan() => this.SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Basic) => 1,
        nameof(SubscriptionType.Pro) => 5,
        nameof(SubscriptionType.Premium) => 10,
        _ => throw new InvalidOperationException(),
    };

    public void ActivateSubscription()
    {
        IsActive = true;
    }

    public void DeactivateSubscription()
    {
        IsActive = false;
    }

#pragma warning disable CS8618
    public Subscription()
    {
    }
#pragma warning restore CS8618
}