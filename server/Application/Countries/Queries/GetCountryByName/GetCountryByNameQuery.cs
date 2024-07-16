using Domain.CountryAggregate;
using ErrorOr;
using MediatR;

namespace Application.Countries.Queries.GetCountryByName;

public record GetCountryByNameQuery(string CountryName) : IRequest<ErrorOr<Country>>;
