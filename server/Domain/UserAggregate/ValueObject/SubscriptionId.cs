namespace Domain.User.ValueObject;

public class SubscriptionId : Common.ValueObject
{
    public Guid Value { get; }

    private SubscriptionId(Guid value)
    {
        Value = value;
    }

    public static SubscriptionId Create(Guid value)
    {
        return new SubscriptionId(value);
    }

    public static SubscriptionId CreateUnique()
    {
        return new SubscriptionId(Guid.NewGuid());
    }


    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    public SubscriptionId()
    {
    }
#pragma warning restore CS8618
}