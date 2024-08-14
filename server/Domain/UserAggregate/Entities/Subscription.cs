using System.Collections.ObjectModel;
using Domain.Common;
using Domain.UserAggregate.Errors;
using ErrorOr;

namespace Domain.User.ValueObject;

public class Subscription : Entity<SubscriptionId>
{
    private readonly List<UserAggregate.User> _users = new();
    private readonly List<UserId> _userIds = new();

    public SubscriptionType SubscriptionType { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public bool IsActive { get; private set; } = false;
    public IReadOnlyList<UserAggregate.User> Users => _users.AsReadOnly();
    public IReadOnlyList<UserId> UserIds => _userIds.AsReadOnly();


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

    public ErrorOr<Updated> AddUser(UserAggregate.User user)
    {
        if (!IsActive) return SubscriptionErrors.SubscriptionIsNotActive;
        if (user.Id == null) return Error.Unexpected(description: "User id is null");
        if (UserIds.Contains(user.Id)) return SubscriptionErrors.UserIsAlreadyOnSubscription;

        if (UserIds.Count >= SubscriptionType.GetMaxPeopleInPlan())
            return SubscriptionErrors.CannotHaveMoreUsersThanTheSubscriptionAllows;

        _users.Add(user);
        _userIds.Add(user.Id);
        return Result.Updated;
    }

    public ErrorOr<Updated> RemoveUser(UserAggregate.User user)
    {
        if (user.Id == null) return Error.Unexpected(description: "User id is null");
        if (!UserIds.Contains(user.Id)) return SubscriptionErrors.UserIsNotOnSubscription;

        _users.Remove(user);
        _userIds.Remove(user.Id);
        return Result.Updated;
    }

    public static ErrorOr<Subscription> Create(SubscriptionType subscriptionType)
    {
        var SubscriptionDuration = subscriptionType.GetSubscriptionDuration(subscriptionType);

        switch (subscriptionType.Name)
        {
            case "Premium":
                return new Subscription(SubscriptionId.CreateUnique(), SubscriptionType.Premium,
                    DateTime.UtcNow.AddYears(SubscriptionDuration));
            case "Pro":
                return new Subscription(SubscriptionId.CreateUnique(), SubscriptionType.Pro,
                    DateTime.UtcNow.AddYears(SubscriptionDuration));
            case "Basic":
                return new Subscription(SubscriptionId.CreateUnique(), SubscriptionType.Basic,
                    DateTime.UtcNow.AddYears(SubscriptionDuration));
            default:
                return Error.Unexpected(description: "This subscription type does not exist");
        }
    }

    // Todo: Move it to SubscriptionType.cs
    // public int GetMaxPostsMade() => this.SubscriptionType.Name switch
    // {
    //     nameof(SubscriptionType.Basic) => 10,
    //     nameof(SubscriptionType.Pro) => int.MaxValue,
    //     nameof(SubscriptionType.Premium) => int.MaxValue,
    //     _ => throw new InvalidOperationException(),
    // };
    //
    // public int GetMaxLikes() => this.SubscriptionType.Name switch
    // {
    //     nameof(SubscriptionType.Basic) => 10,
    //     nameof(SubscriptionType.Pro) => int.MaxValue,
    //     nameof(SubscriptionType.Premium) => int.MaxValue,
    //     _ => throw new InvalidOperationException(),
    // };

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