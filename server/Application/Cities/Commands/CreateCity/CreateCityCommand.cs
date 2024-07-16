using Domain.Cities;
using Domain.CityAggregate;
using ErrorOr;
using MediatR;

namespace Application.Cities.Commands.CreateCity;

public record CreateCityCommand(
    string Name,
    string CountryId,
    IndicatorsCommand? Indicators,
    WeatherCommand? Weather
) : IRequest<ErrorOr<City>>;

public record IndicatorsCommand(
    string CostIndex,
    string? PublicTransportationIndex,
    string? Gasoline,
    string? AverageMonthlyNetSalary
);

public record WeatherCommand(
    string AverageTemperature
);