using Domain.CityAggregate;
using Domain.CountryAggregate;
using ErrorOr;
using MediatR;

namespace Application.Countries.Commands.AddCityToCountry;

public record AddCityToCountryCommand(City City) : IRequest<ErrorOr<Country>>
{
}