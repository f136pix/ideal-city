using System.Text.Json.Serialization;

namespace Contracts.Cities._Messaging;

public class CreateCityQueueRequest : IQueueRequest
{
    public CreateCityQueueRequest(string? cityName, string? country, float? costOfLivingIndex,
        string? publicTransportation, string? gasoline, string? monnthlyNetSalary, string? weather)
    {
        CityName = cityName;
        Country = country;
        CostOfLivingIndex = costOfLivingIndex;
        PublicTransportation = publicTransportation;
        Gasoline = gasoline;
        MonnthlyNetSalary = monnthlyNetSalary;
        Weather = weather;
    }

    [JsonPropertyName("cityName")] public string? CityName { get; set; }

    [JsonPropertyName("country")] public string? Country { get; set; }

    [JsonPropertyName("costOfLivingIndex")]
    public float? CostOfLivingIndex { get; set; }

    [JsonPropertyName("publicTransportation")]
    public string? PublicTransportation { get; set; }

    [JsonPropertyName("gasoline")] public string? Gasoline { get; set; }

    [JsonPropertyName("averageMonthlyNetSalary")]
    public string? MonnthlyNetSalary { get; set; }

    [JsonPropertyName("weather")] public string? Weather { get; set; }
}