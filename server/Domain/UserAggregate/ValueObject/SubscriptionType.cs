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
    
    public int GetSubscriptionDuration(SubscriptionType subscriptionType) => SubscriptionDurations[subscriptionType];
    
    

    public static readonly Dictionary<SubscriptionType, int> SubscriptionDurations = new()
    {
        { SubscriptionType.Premium, 1 },
        { SubscriptionType.Pro, 1 },
        { SubscriptionType.Basic, 100 }
    };
    
    public int GetMaxPeopleInPlan() => this.Name switch
    {
        nameof(Basic) => 1,
        nameof(Pro) => 5,
        nameof(Premium) => 10,
        _ => throw new InvalidOperationException(),
    };


#pragma warning disable CS8618
    private SubscriptionType() : base("", 0)
    {
    }
#pragma warning restore CS8618
}