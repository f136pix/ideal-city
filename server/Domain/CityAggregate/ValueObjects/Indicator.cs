using Domain.Common;

namespace Domain.City.ValueObjects;

public class Indicator : ValueObject
{
    public string CostIndex { get; private set; }
    public string? PublicTransportationIndex { get; private set; }
    public string? Gasoline { get; private set; }
    public string? AverageMonthlyNetSalary { get; private set; }

    private Indicator(string costIndex, string? publicTransportationIndex, string? gasoline,
        string? averageMonthlyNetSalary)
    {
        CostIndex = costIndex;
        PublicTransportationIndex = publicTransportationIndex;
        Gasoline = gasoline;
        AverageMonthlyNetSalary = averageMonthlyNetSalary;
    }

    public static Indicator Create(
        string costIndex,
        string? publicTransportationIndex,
        string? gasoline,
        string? averageMonthlyNetSalary
    )
    {
        // valdiate// domain events
        return new Indicator(
            costIndex,
            publicTransportationIndex,
            gasoline,
            averageMonthlyNetSalary
        );
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return CostIndex;
        yield return PublicTransportationIndex;
        yield return Gasoline;
        yield return AverageMonthlyNetSalary;
    }
    
#pragma warning disable CS8618
    private Indicator()
    {
    }
#pragma warning restore CS8618
}