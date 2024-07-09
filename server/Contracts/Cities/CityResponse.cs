namespace Contracts.Cities;

public record CityResponse(
    string Id,
    string Name,
    string CountryId,
    IndicatorsResponse? Indicators,
    WeatherResponse? Weather,
    List<string>? CityReviewsIds
);

public record IndicatorsResponse(
    string CostIndex,
    string? PublicTransportationIndex,
    string? Gasoline,
    string? AverageMonthlyNetSalary
);

public record WeatherResponse(
    string AverageTemperature
);