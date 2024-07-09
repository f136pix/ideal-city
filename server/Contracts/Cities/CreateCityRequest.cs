namespace Contracts.Cities;

public record CreateCityRequest(
    string Name,
    string CountryId,
    Indicators? Indicators,
    Weather? Weather
);

public record Indicators(
    string CostIndex,
    string? PublicTransportationIndex,
    string? Gasoline,
    string? AverageMonthlyNetSalary
);

public record Weather(
    string AverageTemperature
);