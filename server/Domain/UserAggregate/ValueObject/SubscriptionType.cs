using Ardalis.SmartEnum;

namespace Domain.Common;

public class SubscriptionType : SmartEnum<SubscriptionType>
{
    private SubscriptionType(string name, int value) : base(name, value)
    {
    }

    public static readonly SubscriptionType Basic = new(nameof(Basic), 0);
    public static readonly SubscriptionType Pro = new(nameof(Pro), 1);
    public static readonly SubscriptionType Premium = new(nameof(Premium), 2);

   


#pragma warning disable CS8618
    private SubscriptionType() : base("", 0)
    {
    }
#pragma warning restore CS8618
}