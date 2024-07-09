using Domain.Common;

namespace Domain.City.ValueObjects;

public sealed class CityReviewId : ValueObject
{
    public Guid Value { get; }

    private CityReviewId(Guid value)
    {
        Value = value;
    }

    public static CityReviewId CreateUnique() => new CityReviewId(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public static CityReviewId Create(Guid value)
    {
        return new CityReviewId(value);
    }
    
#pragma warning disable CS8618
    private CityReviewId()
    {
    }
#pragma warning restore CS8618
}