using Domain.Common;

namespace Domain.Cities;

public class Weather : ValueObject
{
    public string AverageTemperature { get; private set; }
    
    public Weather (string averageTemperature)
    {
        AverageTemperature = averageTemperature;
    }
    
    public static Weather Create(string averageTemperature)
    {
        // sends a domain event 
        
        return new Weather(averageTemperature);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return AverageTemperature;
    }
    
#pragma warning disable CS8618
    private Weather()
    {
    }
#pragma warning restore CS8618
}
