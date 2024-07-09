using System.Text.Json.Serialization;
using Domain.Common;

namespace Domain.City.ValueObjects;

public sealed class CityId : ValueObject
{    
    public Guid Value { get; }

    private CityId(Guid value)
    {
        Value = value;
    }

    public static CityId Create (Guid value)
    {
        return new CityId(value);
    }
    
    public static CityId CreateUnique()
    {
        return new CityId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
#pragma warning disable CS8618
    private CityId()
    {
    }
#pragma warning restore CS8618
}