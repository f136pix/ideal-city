using Ardalis.SmartEnum;

namespace Domain.Common;

public class SubscriptionType
    : SmartEnum<SubscriptionType>
{
    private SubscriptionType(string name, int value) : base(name, value)
    {
    }

    public static readonly SubscriptionType Basic = new(nameof(Basic), 0);

    public static readonly SubscriptionType Pro = new(nameof(Pro), 1);

    public int GetMaxPostsAccessed() => Name switch
    {
        nameof(Basic) => 10, // Set the limit for Basic subscription
        nameof(Pro) => int.MaxValue, // Set the limit for Pro subscription
        _ => throw new InvalidOperationException(),
    };

#pragma warning disable CS8618
    private SubscriptionType() : base("", 0)
    {
    }
#pragma warning restore CS8618
}