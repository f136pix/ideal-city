namespace Domain.User.ValueObject;

public class PostId : Common.ValueObject
{
    public Guid Value { get; }

    private PostId(Guid value)
    {
        Value = value;
    }

    public static PostId Create(Guid value)
    {
        return new PostId(value);
    }

    public static PostId CreateUnique()
    {
        return new PostId(Guid.NewGuid());
    }


    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    public PostId()
    {
    }
#pragma warning restore CS8618
}