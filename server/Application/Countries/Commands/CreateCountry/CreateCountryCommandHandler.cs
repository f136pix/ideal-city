using Application._Common.Interfaces;
using Application.Counties.Commands.CreateCountry;
using Domain.CountryAggregate;
using ErrorOr;
using MediatR;

namespace Application.Countries.Commands.CreateCountry;

public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, ErrorOr<Country>>
{
    private readonly ICountryRepository _repository;

    public CreateCountryCommandHandler(ICountryRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ErrorOr<Country>> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
    {
        var country = Country.Create(request.Name);
        
        await _repository.AddAsync(country);
        return country;
    }
}