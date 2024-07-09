namespace Domain.Common.ValueObjects;

public class AverageRating : ValueObject
{
    public double Value { get; private set; }
    public int TotalRatings { get; set; }

    private AverageRating(double value, int totalRatings)
    {
        Value = value;
        TotalRatings = totalRatings;
    }

    public static AverageRating Create(double value = 0, int totalRatings = 0)
    {
        return new AverageRating(value, totalRatings);
    }

    public void AddRating(Rating rating)
    {
        Value = ((Value * TotalRatings) + rating.Value) / TotalRatings++;
    }

    public void RemoveRating(Rating rating)
    {
        Value = ((Value * TotalRatings) - rating.Value) / TotalRatings--;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

#pragma warning disable CS8618
    private AverageRating()
    {
    }
#pragma warning restore CS8618
}

public class Rating : ValueObject
{
    public double Value { get; private set; }

    private Rating(double value)
    {
        Value = value;
    }

    public static Rating Create(double value)
    {
        return new Rating(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
    
#pragma warning disable CS8618
    private Rating()
    {
    }
#pragma warning restore CS8618
}