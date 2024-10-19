using Application._Common.Authorization;
using Domain.CityAggregate;
using Domain.Common;
using ErrorOr;
using MediatR;

namespace Application.Cities.Commands.CreateCity;

// [Authorize(Permissions = "cities:create")]
// [Authorize(Roles = "Admin")]
[Authorize(Subscription = SubscriptionTypeEnum.Basic)]
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