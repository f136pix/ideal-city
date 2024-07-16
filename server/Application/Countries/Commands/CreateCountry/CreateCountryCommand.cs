using Domain.CountryAggregate;
using ErrorOr;
using MediatR;

namespace Application.Counties.Commands.CreateCountry;

public record CreateCountryCommand(
    string Name
) : IRequest<ErrorOr<Country>>;
