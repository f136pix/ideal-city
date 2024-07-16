using Application._Common.Interfaces;
using Domain.CountryAggregate;
using ErrorOr;
using MediatR;

namespace Application.Countries.Queries.GetCountryByName;

public class GetQueryByNameQueryHandler : IRequestHandler<GetCountryByNameQuery, ErrorOr<Country>>
{
    private readonly ICountryRepository _repository;

    public GetQueryByNameQueryHandler(ICountryRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<Country>> Handle(GetCountryByNameQuery request, CancellationToken cancellationToken)
    {
        Country country = await _repository.GetByProperty("Name", request.CountryName);

        if (country is null)
        {
            return Error.NotFound(description: "Country with given name not found");
        }

        return country;
    }
}