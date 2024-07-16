using Domain.Cities;
using Domain.City.ValueObjects;
using Domain.CityAggregate;
using Domain.Countries;
using Domain.CountryAggregate;
using ErrorOr;
using MediatR;

namespace Application.Countries.Commands.AddCityToCountry;

public record AddCityToCountryCommand(City City) : IRequest<ErrorOr<Country>>;