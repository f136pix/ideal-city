using Domain.Common;

namespace Domain.Countries;

public sealed class CountryId : ValueObject
{
    public Guid Value { get; }

    private CountryId(Guid value)
    {
        Value = value;
    }
    
    public static CountryId Create(Guid value)
    {
        return new CountryId(value);
    }
    
    public static CountryId Create(string value)
    {
        return new CountryId(Guid.Parse(value));
    }

    public static CountryId CreateUnique()
    {
        return new CountryId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
#pragma warning disable CS8618
    private CountryId()
    {
    }
#pragma warning restore CS8618
}